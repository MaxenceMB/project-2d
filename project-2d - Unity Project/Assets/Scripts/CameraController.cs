using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private GameObject level;
    [SerializeField] private float smoothValue;

    private Vector3 velocity = Vector3.zero;

    private float minX, maxX, minY, maxY;


    private void Start()
    {
        // Getting camera's dimensions
        Camera cam = GetComponent<Camera>();
        float camHeightDimensions = 2f * cam.orthographicSize;
        float camWidthDimensions = camHeightDimensions * cam.aspect;

        // Getting level's dimensions
        TilemapRenderer levelRenderer = level.GetComponent<TilemapRenderer>();

        // Assigning values to the borders for the camera
        minX = levelRenderer.bounds.min.x + camWidthDimensions / 2f;
        maxX = levelRenderer.bounds.max.x - camWidthDimensions / 2f;
        minY = levelRenderer.bounds.min.y + camHeightDimensions / 2f;
        maxY = levelRenderer.bounds.max.y - camHeightDimensions / 2f;
    }

    private void FixedUpdate()
    {
        // Smoothing the camera's movement
        Vector3 finalPos = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref velocity, smoothValue);

        // Blocking camera with borders
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            -10
        );
    }
}