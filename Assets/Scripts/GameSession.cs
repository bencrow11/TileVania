using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3; // The amount of lives the player has.
    [SerializeField] int score = 0; // The score the player has.
    [SerializeField] TextMeshProUGUI livesText; // The text that shows the lives.
    [SerializeField] TextMeshProUGUI scoreText; // Text that shows the score.

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

    void Start()
    {
        livesText.text = playerLives.ToString(); // Updates UI to show lives.
        scoreText.text = score.ToString(); // Updates UI to show score.
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

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd; // Adds the points to the score.
        scoreText.text = score.ToString(); // Updates UI.
    }

    void TakeLife()
    {
        playerLives--; // Removes 1 life from playerLives.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Get the current scene index.
        SceneManager.LoadScene(currentSceneIndex); // Reload the current scene.
        livesText.text = playerLives.ToString(); // Updates UI.
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist(); // Resets the scene persist.
        SceneManager.LoadScene(0); // Sets the scene to the first one.
        Destroy(gameObject); // Destroy the current game instance.
    }
}
