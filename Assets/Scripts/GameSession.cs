using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3; // The amount of lives the player has.

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length; // Gets all GameSession objects.

        if (numGameSessions > 1)
        {
            // If there is already one that exists, destroy this one.
            Destroy(gameObject);
        }
        else
        {
            // Otherwise, don't destroy it.
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        // If the player still has lives left, just remove one.
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            // Otherwise, reset the game.
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        playerLives--; // Removes 1 life from playerLives.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Get the current scene index.
        SceneManager.LoadScene(currentSceneIndex); // Reload the current scene.
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0); // Sets the scene to the first one.
        Destroy(gameObject); // Destroy the current game instance.
    }
}
