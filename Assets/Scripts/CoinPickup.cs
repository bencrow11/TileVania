using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX; // Audio to play when the player picks up a coin.
    [SerializeField] int pointsPerPickup = 100; // Score to give player when they pick up a coin.

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the thing that entered the trigger is a player:
        if (other.tag == "Player")
        {
            // Adds the score to the player.
            FindObjectOfType<GameSession>().AddToScore(pointsPerPickup);

            // Play the pickup audio.
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);

            // Destroy this game object.
            Destroy(gameObject);
        }
    }
}
