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
    [SerializeField] float jumpSpeed = 5f; // Speed the player should jump at.
    [SerializeField] float climbSpeed = 5f; // Speed the player should climb ladders.
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f); // The velocity of the death position.
    [SerializeField] GameObject bullet; // The bullet the player can shoot.
    [SerializeField] Transform gun; // the gun that shoots the bullet.
    float gravityScaleAtStart; // The gravity of the player.
    CapsuleCollider2D myBodyCollider; // Collider to check if player is touching objects.
    BoxCollider2D myFeetCollider; // The players collider for their feet.
    bool isAlive = true; // Is the player alive?

    void Start()
    {
        // Initialise all fields.
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) // If the isn't alive, just return.
        {
            return;
        }

        // Move and flip the sprite, if the player is moving.
        Run();
        FlipSprite();
        ClimbLadder();
        StartCoroutine(Die());
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) // If the isn't alive, just return.
        {
            return;
        }

        Instantiate(bullet, gun.position, transform.rotation); // Shoot a bullet.
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) // If the isn't alive, just return.
        {
            return;
        }

        // Set the move input to the input vector value.
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) // If the isn't alive, just return.
        {
            return;
        }

        // If the player isn't touching the ground, just return.
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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

    void ClimbLadder()
    {

        // If the player isn't touching a ladder, just return.
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidBody.gravityScale = gravityScaleAtStart; // Sets the gravity scale to default if the player isn't climbing.
            myAnimator.SetBool("isClimbing", false); // Stops the climbing animation.
            return;
        }

        // Sets the players velocity if they're climbing a ladder.
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f; // Sets the gravity to 0 so they don't fall down it.

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;

        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed); // Sets the climbing animation if the player is moving vertically.
    }

    IEnumerator Die()
    {
        // If the player is touching an enemy, kill them.
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false; // Sets isAlive to false.
            myAnimator.SetTrigger("Dying"); // Set the animation to dying.
            myRigidBody.velocity = deathKick;

            yield return new WaitForSecondsRealtime(1f); // wait the delay.

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
