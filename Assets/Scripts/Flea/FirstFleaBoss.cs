using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFleaBoss : Enemy
{
    [Header("Boss - Health")]
    [SerializeField] private int bossMaxHealth = 60;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;

    [Header("Jump / Attack base values")]
    [SerializeField] private float baseJumpVertical = 10f;
    [SerializeField] private float baseJumpHorizontal = 4f;
    [SerializeField] private float baseJumpDuration = 0.8f;
    [SerializeField] private float baseJumpCooldown = 3f;
    [SerializeField] private float landingDamageRadius = 1f;
    [SerializeField] private int landingDamage = 6;

    [Header("Phase system (ordered)")]
    [SerializeField] private List<Phase> phases = new List<Phase>();

    private bool isPreparing = false;
    private bool isInAir = false;
    private bool canAct = true;

    private bool jumpForceApplied = false;
    private bool landedEventCalled = false;
    private Vector2 lastJumpTarget;

    [System.Serializable]
    public class Phase
    {
        [Range(0f, 1f)] public float minHealthPercent = 0f;
        public bool enableJump = true;
        public float jumpCooldownMultiplier = 1f;
        public float jumpVerticalMultiplier = 1f;
        public float jumpHorizontalMultiplier = 1f;
        public float landingDamageMultiplier = 1f;
        public bool spawnMinions = false;
        public GameObject minionPrefab;
        public int minionsToSpawn = 0;
    }

    private int currentPhaseIndex = 0;

    protected override void Start()
    {
        base.Start();

        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (player == null)
        {
            var pgo = GameObject.FindGameObjectWithTag("Player");
            if (pgo) player = pgo.transform;
        }

        health = bossMaxHealth;
        phases.Sort((a, b) => b.minHealthPercent.CompareTo(a.minHealthPercent));
        EvaluatePhase(force: true);
    }

    private void Update()
    {
        if (player == null) return;

        EvaluatePhase();

        var phase = GetCurrentPhase();
        if (canAct && !isPreparing && !isInAir && phase.enableJump)
        {
            StartCoroutine(JumpCycle());
        }

        animator?.SetBool("IsPreparing", isPreparing);
        animator?.SetBool("IsInAir", isInAir);
    }

    private Phase GetCurrentPhase()
    {
        if (phases.Count == 0)
            return new Phase() { minHealthPercent = 0f, enableJump = true, jumpCooldownMultiplier = 1f, jumpVerticalMultiplier = 1f, jumpHorizontalMultiplier = 1f, landingDamageMultiplier = 1f };
        return phases[Mathf.Clamp(currentPhaseIndex, 0, phases.Count - 1)];
    }

    private void EvaluatePhase(bool force = false)
    {
        float ratio = bossMaxHealth <= 0 ? 0f : (float)health / (float)bossMaxHealth;
        int best = 0;
        for (int i = 0; i < phases.Count; i++)
        {
            if (ratio >= phases[i].minHealthPercent)
            {
                best = i;
                break;
            }
        }

        if (best != currentPhaseIndex || force)
        {
            currentPhaseIndex = best;
            OnEnterPhase(currentPhaseIndex);
        }
    }

    private void OnEnterPhase(int idx)
    {
        Phase p = GetCurrentPhase();
        if (p.spawnMinions && p.minionPrefab != null && p.minionsToSpawn > 0)
        {
            StartCoroutine(SpawnMinions(p.minionsToSpawn, p.minionPrefab));
        }
    }

    private IEnumerator SpawnMinions(int n, GameObject prefab)
    {
        for (int i = 0; i < n; i++)
        {
            Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle * 1.5f;
            Instantiate(prefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void FacePlayer()
    {
        if (player == null) return;

        Vector3 localScale = transform.localScale;
        if (player.position.x < transform.position.x)
            localScale.x = -Mathf.Abs(localScale.x); // olha pra esquerda
        else
            localScale.x = Mathf.Abs(localScale.x);  // olha pra direita
        transform.localScale = localScale;
    }

    private IEnumerator JumpCycle()
    {
        Phase phase = GetCurrentPhase();

        canAct = false;
        isPreparing = true;
        jumpForceApplied = false;
        landedEventCalled = false;

        lastJumpTarget = player.position;

        // Vira para o player antes de pular
        FacePlayer();

        animator?.SetTrigger("PrepareJump");

        yield return new WaitUntil(() => jumpForceApplied == true);

        isPreparing = false;
        isInAir = true;

        yield return new WaitUntil(() => landedEventCalled == true);

        // Cooldown do pulo
        float cooldown = baseJumpCooldown * phase.jumpCooldownMultiplier;
        yield return new WaitForSeconds(cooldown);

        isInAir = false;
        canAct = true;
    }

    public void ApplyJumpForceEvent()
    {
        Phase phase = GetCurrentPhase();
        lastJumpTarget = player.position;

        float horizMultiplier = baseJumpHorizontal * phase.jumpHorizontalMultiplier;
        float jumpHeight = baseJumpVertical * phase.jumpVerticalMultiplier;
        float duration = Mathf.Max(0.1f, baseJumpDuration * phase.jumpCooldownMultiplier);

        StartCoroutine(DoArcJump(lastJumpTarget, jumpHeight, duration, horizMultiplier));

        jumpForceApplied = true;
    }

    private IEnumerator DoArcJump(Vector2 target, float jumpHeight, float duration, float horizMultiplier)
    {
        Vector2 start = transform.position;
        float elapsed = 0f;
        Vector2 mid = (start + target) * 0.5f;
        mid += Vector2.up * jumpHeight;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            Vector2 a = Vector2.Lerp(start, mid, t);
            Vector2 b = Vector2.Lerp(mid, target, t);
            Vector2 pos = Vector2.Lerp(a, b, t);

            transform.position = new Vector3(pos.x, pos.y, 0f);

            float normalizedHeight = (jumpHeight <= 0f) ? 0f : Mathf.Clamp01(Vector2.Distance(pos, Vector2.Lerp(start, target, t)) / jumpHeight);
            animator?.SetFloat("JumpHeight", normalizedHeight);

            yield return null;
        }

        OnLandingEvent();
    }

    public void OnLandingEvent()
    {
        landedEventCalled = true;

        // Aplica dano ao player quando aterrissa
        Phase phase = GetCurrentPhase();
        int dmg = Mathf.CeilToInt(landingDamage * phase.landingDamageMultiplier);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, landingDamageRadius, playerLayer);
        foreach (var h in hits)
            h.GetComponent<PlayerHealth>()?.TakeDamage(dmg);

        // Trigger da animação de aterrissagem
        animator?.SetTrigger("Land");

        // Vira para o player novamente depois de aterrissar
        FacePlayer();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, landingDamageRadius);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        EvaluatePhase();
    }
}
