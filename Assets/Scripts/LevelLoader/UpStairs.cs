using UnityEngine;

public class UpStairs : MonoBehaviour
{
    public LevelLoader levelLoader;
    public int floor;
    public string entryOrStairs;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        levelLoader.LoadFloor(floor, entryOrStairs);
    }
}
