using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour {

    public DungeonRoomDisplayer roomDisplayer;

    private void OnTriggerEnter2D(Collider2D other) {
        Camera camera = FindObjectOfType<Camera>();
        camera.GetComponent<DungeonCameraController>().PlaceCameraAtRoom(transform.parent.gameObject);
    }

    

}
