using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public Rigidbody2D rigidBody;
    public float speed = 0.05f;
    float direction;

    void Start(){
        float direction = 0;
    }

    void FixedUpdate(){
        direction = 0;
        if (Input.GetKey(KeyCode.W)){
            rigidBody.velocity = new Vector2(0, speed * direction);
        } 
        if (Input.GetKey(KeyCode.S)){
            rigidBody.velocity = new Vector2(0, -speed);
        } 
        if (Input.GetKey(KeyCode.A)){
            rigidBody.velocity = new Vector2(-speed, 0);
        } 
        if (Input.GetKey(KeyCode.D)){
            rigidBody.velocity = new Vector2(-speed, 0);
        } 

        /*
        float directionX = Input.GetAxis("Horizontal");
        directionX = Mathf.Round(directionX);
        rigidBody.velocity = new Vector2(speed * directionX, rigidBody.velocity.y);*/
    }

}
