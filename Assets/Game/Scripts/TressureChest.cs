using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TressureChest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Movements playerController = other.gameObject.GetComponent<Movements>();
            if (playerController != null)
            {
                playerController.CollectTreasure();
                gameObject.SetActive(false); // Deactivate the treasure chest
            }
        }
    }
}
