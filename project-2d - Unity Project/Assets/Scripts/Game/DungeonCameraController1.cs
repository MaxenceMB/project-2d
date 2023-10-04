using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonCameraController : MonoBehaviour {

    public int currentRoomID = 0;

    [SerializeField] private float smoothValue = 0.15f;
    private Vector3 velocity = Vector3.zero;

    public void PlaceCameraAtRoom(GameObject room){
        if (currentRoomID != room.GetComponent<DungeonRoomDisplayer>().roomID){
            currentRoomID = room.GetComponent<DungeonRoomDisplayer>().roomID;
            Vector3 finalPos = new Vector3(room.transform.position.x, room.transform.position.y, - 10);
            StartCoroutine(SmoothCamera(finalPos));
        }
    }

    IEnumerator SmoothCamera(Vector3 finalPos){
        float distanceBetween = Mathf.Sqrt(Mathf.Pow(transform.position.x - finalPos.x, 2) + Mathf.Pow(transform.position.y - finalPos.y, 2));
        while (distanceBetween > 0.03f){
            transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref velocity, smoothValue);
            distanceBetween = Mathf.Sqrt(Mathf.Pow(transform.position.x - finalPos.x, 2) + Mathf.Pow(transform.position.y - finalPos.y, 2));
            yield return null;
        }
        transform.position = finalPos;
    }
}