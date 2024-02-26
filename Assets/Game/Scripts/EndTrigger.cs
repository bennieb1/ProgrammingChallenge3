using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class EndTrigger :  Menu
{ // Use this to play the sound
    public float delayBeforeLoading = 2f; // Delay in seconds before loading the next scene
    public SceneReference LevelToLoad;
    public MenuClassifier hudClassifier;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.gameObject.CompareTag("Player")) // Ensure your player has the tag "Player"
        {
            // Play the sound effect using the AudioSource
          
            SceneManager.LoadScene(LevelToLoad);
            MenuManager.Instance.ShowMenu(hudClassifier);
            Debug.Log("Player reached the end of the level!");

            // Start the coroutine to load the next scene
            
        }
    }

    IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
