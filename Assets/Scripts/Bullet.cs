using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidBody; // The body of the bullet.
    [SerializeField] float bulletSpeed = 20f; // The speed the bullet should travel.
    PlayerMovement player; // The player.
    float xSpeed; // The speed and direction of the bullet.

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the bullet has hit an enemy, kill it.
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        // Destroy the bullet.
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // When the bullet collides with something, destroy it.
        Destroy(gameObject);
    }
}
