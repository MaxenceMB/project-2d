using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Shop", menuName = "Interactibles/New Shop")]
public class ShopObject : ScriptableObject {
    
    [Header("Shop Object")]
    [SerializeField] private string title;
    private GameObject canvas;
    private GameObject pouch;

    public void Start() {
        canvas = GameObject.Find("ShopCanvas");
        pouch = GameObject.Find("PouchManager");
    }

    public void Interact(Canvas canvas, GameObject pouch) {
    }

}
