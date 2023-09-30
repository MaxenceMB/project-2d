using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

    [SerializeField] Rigidbody2D rb;
    [SerializeField] public Arrow arrow;

    /// <summary>
    /// Destroys the arrow 4 seconds after spawning it in
    /// </summary>
    void Start(){
        Destroy(gameObject, 4f);
    }


    void FixedUpdate(){
        // Continuously moves in a direction based on the arrow velocity
        rb.velocity = transform.right * arrow.velocity;
    }


    /// <summary>
    /// On collision with an enemy, the arrow deals damage and is destroyed
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")){
            other.gameObject.GetComponent<EnemyManager>().TakeDamage(arrow.damage);
        } 
        Destroy(gameObject);
    }

}
