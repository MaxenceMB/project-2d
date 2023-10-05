using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour {

    [Header("Game objects")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject level;

    [Header("Cam values")]
    [SerializeField] private float smoothValue;
    [SerializeField] private float viewHeight;

    [HideInInspector] public static float camHeightDimensions, camWidthDimensions;

    private Vector3 velocity = Vector3.zero;
    private float minX, maxX, minY, maxY;
    private bool inCinematic = false;
    private Camera cam;


    private void Start() {

        // Getting camera's dimensions
        cam = GetComponent<Camera>();
        camHeightDimensions = 2f * cam.orthographicSize;
        camWidthDimensions = camHeightDimensions * cam.aspect;

        // Getting level's dimensions
        TilemapRenderer levelRenderer = level.GetComponent<TilemapRenderer>();

        // Assigning values to the borders for the camera
        minX = levelRenderer.bounds.min.x + camWidthDimensions  / 2f;
        maxX = levelRenderer.bounds.max.x - camWidthDimensions  / 2f;
        minY = levelRenderer.bounds.min.y + camHeightDimensions / 2f;
        maxY = levelRenderer.bounds.max.y - camHeightDimensions / 2f;

    }


    private void FixedUpdate() {

        // If the camera is allowed to move
        if(!inCinematic) {

            // Smoothing the camera's movement
            Vector3 finalPos = new Vector3(player.position.x, player.position.y + viewHeight, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref velocity, smoothValue);

            BlockPosition();
        }
    }


    private void BlockPosition() {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            -10
        );
    }


    public void SmoothFocus(GameObject obj) {
        Vector3 startPos = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        Vector3 finalPos = new Vector3(obj.transform.position.x, obj.transform.position.y, -10);

        StartCoroutine(SmoothFocusToPoint(startPos, finalPos));
    }

    private IEnumerator SmoothFocusToPoint(Vector3 start, Vector3 end) {
        inCinematic = true;
        player.GetComponent<PlayerMovement>().SetCanMove(false);
        float ortho = cam.orthographicSize;

        while(Vector3.Distance(transform.position, end) > 0.01f) {
            this.transform.position = Vector3.SmoothDamp(transform.position, end, ref velocity, 0.5f);
            if(cam.orthographicSize < ortho+0.5f) cam.orthographicSize += 0.01f;
            yield return null;
        }

        this.transform.position = end;
        yield return new WaitForSecondsRealtime(3f);

        while(Vector3.Distance(transform.position, start) > 0.01f) {
            this.transform.position = Vector3.SmoothDamp(transform.position, start, ref velocity, 0.5f);
            yield return null;
        }

        this.transform.position = start;

        while(cam.orthographicSize > ortho) {
            cam.orthographicSize -= 0.01f;
            yield return null;
        }

        player.GetComponent<PlayerMovement>().SetCanMove(true);
        inCinematic = false;
    }
}