using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f; // The time in seconds to wait before loading the next level.

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") // If the player triggers the collider:
        {
            StartCoroutine(LoadNextLevel()); // When the exit is triggered, start a coroutine that will load the next level.
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay); // wait the delay.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Get the current scene index.
        int nextSceneIndex = currentSceneIndex + 1; // Gets the index of the next scene.


        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // If the next scene index is the same as scene count (no next index), reset it to 0.
        }

        SceneManager.LoadScene(nextSceneIndex); // Load the next scene.
    }
}
