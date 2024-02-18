using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f; // The speed the enemy moves.
    Rigidbody2D myRigidBody; // The body of the enemy.

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>(); // Initialises the rigidbody;
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(speed, 0f); // Sets the move speed.
    }

    void OnTriggerExit2D(Collider2D other)
    {
        speed = -speed; // When the trigger is exited (gets to the end of the path), change directions.
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f); // Changes the direction the enemy is facing.
    }
}
