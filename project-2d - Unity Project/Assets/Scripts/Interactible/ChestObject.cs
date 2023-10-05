using UnityEngine;

[CreateAssetMenu(fileName = "New_Chest", menuName = "Interactibles/New Chest")]
public class ChestObject : ScriptableObject {
    
    [Header("Chest Object")]
    [SerializeField] private string title;

    [Header("Chest ")]
    [SerializeField] private Item[] item;

    public void Interact() {
        GameObject.Find("Main Camera").GetComponent<CameraController>().SmoothFocus(GameObject.Find("FOCUS POINT"));
    }

}
