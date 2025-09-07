using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpperDoors : MonoBehaviour {

    public Tilemap tilemap;
    public TileBase openDoorUp;
    public TileBase closeDoorUp;
    private Vector3Int doorOne = new Vector3Int(18, 2, 0);
    private Vector3Int doorTwo = new Vector3Int(24, 2, 0);


    public void openDoor(Vector3Int hitPosition)
    {
        tilemap.SetTile(new Vector3Int(hitPosition.x, hitPosition.y + 1, hitPosition.z), openDoorUp);
    }

    public void closeDoor(Vector3Int hitPosition)
    {
        tilemap.SetTile(new Vector3Int(hitPosition.x, hitPosition.y + 1, hitPosition.z), closeDoorUp);
        
    }
}
