using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonCameraController : MonoBehaviour {

    public GameObject currentRoom;

    private void FixedUpdate() {
        if (currentRoom != null){
            transform.position = currentRoom.transform.position;
        }
    }
}