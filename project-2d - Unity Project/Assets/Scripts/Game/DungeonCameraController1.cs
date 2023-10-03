using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonCameraController : MonoBehaviour {

    [Header("Game objects")]
    [SerializeField] private Transform player;

    [Header("Cam values")]
    [SerializeField] private float smoothValue;
    [SerializeField] private float viewHeight;

    [HideInInspector] public static float camHeightDimensions, camWidthDimensions;

    private Vector3 velocity = Vector3.zero;
    private float minX, maxX, minY, maxY;


    private void Start() {

        // Getting camera's dimensions
        Camera cam = GetComponent<Camera>();
        camHeightDimensions = 2f * cam.orthographicSize;
        camWidthDimensions = camHeightDimensions * cam.aspect;

    }


    private void FixedUpdate() {

        // Smoothing the camera's movement
        Vector3 finalPos = new Vector3(player.position.x, player.position.y + viewHeight, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref velocity, smoothValue);

    }
}