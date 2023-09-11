using UnityEngine;
using System;

public class PNJBehaviour : MonoBehaviour {

    [SerializeField] private float detectionDistance;

    private void Awake() {
        this.GetComponent<CircleCollider2D>().radius = detectionDistance;
    }

    public void Interact() {
        Debug.Log("OUI");
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }
}
