using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    [SerializeField] public float arrowVelocity;
    [SerializeField] Rigidbody2D rb;

    void Start(){
        Destroy(gameObject, 4f);
    }

    void FixedUpdate(){
        rb.velocity = transform.right * arrowVelocity;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")){
            Debug.Log("Oui");
            other.gameObject.GetComponent<EnemyManager>().TakeDamage(10f);
        } 
        Debug.Log("Non");
        Destroy(gameObject);
    }

}
