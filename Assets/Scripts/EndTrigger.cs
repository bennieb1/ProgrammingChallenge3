using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public AudioClip soundEffect; // Assign this in the inspector
    private AudioSource globalAudioSource; // Use this to play the sound

    void Start()
    {
        // Ensure there's an AudioSource component attached to this GameObject
       
        if (globalAudioSource == null)
        {
            // If AudioSource is not found, add one dynamically
            globalAudioSource = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.gameObject.CompareTag("Player")) // Ensure your player has the tag "Player"
        {
            

            // Play the sound effect using the AudioSource
            globalAudioSource = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();

            globalAudioSource.PlayOneShot(soundEffect);
            Debug.Log("Player reached the end of the level!");

        }
    }

}
