using Unity.VisualScripting;
using UnityEngine;

public class UpperDoors1 : MonoBehaviour
{
    private bool playerDetected;
    public Transform firstDoorPos;
    public Transform secondDoorPos;
    public float firstDoorWidth;
    public float firstDoorHeight;
    public float secondDoorWidth;
    public float secondDoorHeight;

    public LayerMask player;

    private void Update()
    {
        playerDetected = Physics2D.OverlapBox(firstDoorPos.position, new Vector2(firstDoorWidth, firstDoorHeight), 0, player) 
            || Physics2D.OverlapBox(secondDoorPos.position, new Vector2(secondDoorWidth, secondDoorHeight), 0, player);

        if (playerDetected == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(firstDoorPos.position, new Vector3(firstDoorWidth, firstDoorHeight, 1));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(secondDoorPos.position, new Vector3(secondDoorWidth, secondDoorHeight, 1));
    }
}
