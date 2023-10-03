using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int speed;
    [HideInInspector] public bool canMove = true;

    [Header("Dash")]
    [Range(0, 10)][SerializeField] private float dashForce;
    [Range(0, 10)][SerializeField] private float dashTime; 
    private bool dashing = false;
    private float currentDashTime;    
    private Vector2 dashDirection, dashMovement;


    private void Update(){

        // Movement
        if(canMove && !dashing) {

            // Get player's input direction
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");

            // Moves the player according to it
            rb.velocity = new Vector2(speed * directionX, rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x, speed * directionY);

            // If the player moves on both x and y axis, make it slower
            if(Math.Abs(directionX) + Math.Abs(directionY) == 2 ) {
                rb.velocity = rb.velocity/(float)Math.Sqrt(2);
            }

            // If the player presses Space and he is moving
            if(Input.GetKeyDown(KeyCode.Space) && rb.velocity != new Vector2(0, 0)) {
                this.dashDirection = rb.velocity;   // Saves the direction of the dash
                this.currentDashTime = 0f;          // Instantiate dash's start time
                this.dashing = true;                // Put dashing to true for not letting the player move in same time
            }

        } else if(dashing) {

            // If dash didn't end yet
            if(currentDashTime < dashTime) {
                // Move the player and increases the time
                dashMovement = dashDirection * dashForce;
                currentDashTime += 0.1f;

                this.transform.localScale = new Vector2(1, 0.75f);
            } else {
                // After end
                dashMovement = Vector2.zero;
                dashing = false;

                this.transform.localScale = new Vector2(1, 1);
            }
            rb.velocity = dashMovement;

        } else {
            rb.velocity = new Vector2(0, 0);
        }
    }

    /// <summary>
    /// sets the movement speed of the player based on his speed stat
    /// </summary>
    /// <param name="spd"> the speed stat </param>
    public void setSpeed(int spd) {
        this.speed = spd;
    }

}
