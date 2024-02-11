using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    Class that controls all of the players movements.
*/
public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput; // The vector of the player, from the input.
    Rigidbody2D myRigidBody; // The body of the player.
    Animator myAnimator; // Animator to change animations based on movement.
    [SerializeField] float runSpeed = 10f; // Speed the player should move at.
    [SerializeField] float jumpSpeed = 5; // Speed the player should jump at.
    CapsuleCollider2D myCapsuleCollider; // Collider to check if player is touching objects.

    void Start()
    {
        // Initialise all fields.
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // Move and flip the sprite, if the player is moving.
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        // Set the move input to the input vector value.
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        // If the player isn't touching the ground, just return.
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        // If the button is pressed, add jump speed to the vector.
        if (value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        // Sets the players velocity based on the run speed.
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        // Checks the player is moving horizontally.
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        // Sets the isRunning animation based on if the player is running or idle.
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        // Checks the player is moving horizontally.
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        // If the player is moving horizontally:
        if (playerHasHorizontalSpeed)
        {
            // Find the direction and transform the player to face the correct way.
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }

    }
}
