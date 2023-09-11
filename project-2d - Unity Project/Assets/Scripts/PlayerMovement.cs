using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int speed;

    void Update(){

        // Movement
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(speed * directionX, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, speed * directionY);

        if(Math.Abs(directionX) + Math.Abs(directionY) == 2 ) {
            rb.velocity = rb.velocity/(float)Math.Sqrt(2);
        }
    }

}
