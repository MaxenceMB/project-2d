using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public Rigidbody2D rb;
    public float runSpeed;

    void FixedUpdate(){
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(runSpeed * directionX, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, runSpeed * directionY);
    }

}
