using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public Rigidbody2D rb;

    void FixedUpdate(){
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(PlayerManager.instance.PlayerSpeed * directionX, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, PlayerManager.instance.PlayerSpeed * directionY);
    }

}
