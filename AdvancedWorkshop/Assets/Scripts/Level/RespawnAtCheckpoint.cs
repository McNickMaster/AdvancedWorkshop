using UnityEngine;

public class RespawnAtCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the player has a PlayerController script.
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // Respawn the player at the last checkpoint.
                playerController.RespawnAtLastCheckpoint();
            }
        }
    }
}
