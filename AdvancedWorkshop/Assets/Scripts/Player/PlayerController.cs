using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 respawnPosition;

    private void Start()
    {
        // Initialize the respawn position to the player's starting position.
        respawnPosition = transform.position;
    }

    private void Update()
    {
        // You can implement your player controls here.
    }

    // Call this function to respawn the player at the last checkpoint.
    public void RespawnAtLastCheckpoint()
    {
        transform.position = respawnPosition;
    }
}
