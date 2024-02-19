using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length; // Gets all ScenePersist objects.

        if (numScenePersists > 1)
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

    public void ResetScenePersist()
    {
        // Destroys this game object.
        Destroy(gameObject);
    }
}
