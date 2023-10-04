using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Vector3 lastCheckpointPosition; // Store the position of the last checkpoint.
    private bool isDead = false; // Flag to track whether the player is dead.

    private void Awake()
    {
        instance = this; 
        // Initialize the last checkpoint position to the initial spawn point.
    }

    private void Start()
    {

        lastCheckpointPosition = PlayerMovement.instance.transform.position;
    }

    private void Update()
    {
        if (isDead)
        {
            // You can implement a game over or respawn UI here if needed.
            // For now, you can respawn the player at the last checkpoint.
            RespawnAtLastCheckpoint();
        }
        else
        {
            // Handle player controls and gameplay logic here.
        }
    }

    // Call this method to respawn the player at the last checkpoint.
    public void RespawnAtLastCheckpoint()
    {
        // Set the player's position to the last checkpoint position.
        transform.position = lastCheckpointPosition;

        PlayerMovement.instance.rb.velocity = Vector3.zero;

        // Set isDead to false to indicate that the player is no longer dead.
        isDead = false;
    }

    // Call this method when the player dies.
    public void Die()
    {
        // Perform any death-related logic here (e.g., play death animation).

        // Set isDead to true to indicate that the player is dead.
        isDead = true;
    }

    // Call this method when the player reaches a checkpoint.
    public void SetCheckpoint(Vector3 checkpoint)
    {
        // Update the last checkpoint position.
        lastCheckpointPosition = checkpoint;
    }
}