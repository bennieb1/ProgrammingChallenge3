using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 startPosition;
    private float delay = 10000000.0f; // Initial delay, can be adjusted if needed
    private bool isRespawning = false; // Flag to track respawn status

    void Start()
    {
        startPosition = transform.position; // Save start position for respawning
    }

    void Update()
    {
        if (transform.position.y < -5 && !isRespawning) // Check if the player has fallen and is not already waiting to respawn
        {
            isRespawning = true; // Indicate that the respawn process has started
            delay = 10000000.0f; // Set the desired delay for respawn
        }

        if (isRespawning)
        {
            if (delay > 0)
            {
                delay -= 1.0f; // Decrease delay over time
            }
            else
            {
                RespawnPlayer();
                isRespawning = false; // Reset respawn status after respawning
            }
        }
    }

    public void RespawnPlayer()
    {
        transform.position = startPosition; // Move player back to start position
        // Reset velocity to prevent falling death loop
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
