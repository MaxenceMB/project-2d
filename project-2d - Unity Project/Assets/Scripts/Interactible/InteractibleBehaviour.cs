using UnityEngine;

public enum Interactible { PNJ, Chest, NotInteractible };

public class InteractibleBehaviour : MonoBehaviour {

    [SerializeField] private float detectionDistance;
    [SerializeField] private Interactible type;

    private void Start() {
        this.GetComponent<CircleCollider2D>().radius = detectionDistance;
    }

    public void Interact() {
        switch(type) {
            case Interactible.PNJ:
                Debug.Log("PNJ");
                break;

            case Interactible.Chest:
                Debug.Log("CHEST");
                this.type = Interactible.NotInteractible;
                break;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }
}
