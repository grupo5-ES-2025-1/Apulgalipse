using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator crossfade;
    public float transitionTime = 2f;

    public void LoadFloor(int floor, string entryOrStairs)
    {
        crossfade.SetTrigger("Start");

        new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Floor " + floor + " - " + entryOrStairs);
    }
}
