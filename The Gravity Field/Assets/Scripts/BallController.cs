﻿using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 movement, relForce;
    FieldManager fm;
    int jumpSpeed = 350;
    int moveSpeed = 4;
    float maxVelocity = 5.0f;
    bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fm = GetComponentInChildren<FieldManager>();
        relForce = Vector3.zero;
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * 2.0f + relForce);

        float moveHorizontal = Input.GetAxis ("Horizontal");
        // move left or right
        if (moveHorizontal < 0) 
            MoveLeft();
        else if (moveHorizontal > 0)        
            MoveRight();

        // Limit the speed of the ball
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity), rb.velocity.y, rb.velocity.z);

    }

    public void ResetMove()
    {
        movement.x = 0.0f;
        movement.y = 0.0f;
    }
    public void MoveLeft() { movement.x = (-1) * moveSpeed; }
    public void MoveRight() { movement.x = moveSpeed; }

    public void Jump()
    {
        // Jumping is only available when the ball is on the ground
        if (!isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpSpeed);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        // If the ball hits the ground, let 'isJumping' be false
        if (isJumping && (c.gameObject.tag == "MAP" || c.gameObject.tag == "OBSTACLE"))
            isJumping = false;
    }

    public void SetRelativeForce(Vector3 relForce) { this.relForce = relForce; }
}
