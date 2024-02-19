using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the thing that entered the trigger is a player:
        if (other.tag == "Player")
        {
            // Play the pickup audio.
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);

            // Destroy this game object.
            Destroy(gameObject);
        }
    }
}
