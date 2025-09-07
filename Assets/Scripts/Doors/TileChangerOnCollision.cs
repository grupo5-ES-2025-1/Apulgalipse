using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorTileChangerOnCollision : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase openDoorDownTile;
    public TileBase closeDoorDown;

    public UpperDoors upperDoors;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3Int hitPosition = tilemap.WorldToCell(collision.GetContact(0).point);

        tilemap.SetTile(hitPosition, openDoorDownTile);
        upperDoors.openDoor(hitPosition);

        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        new WaitForSeconds(2);

        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        tilemap.SetTile(hitPosition, closeDoorDown);
        upperDoors.closeDoor(hitPosition);
    }
}
