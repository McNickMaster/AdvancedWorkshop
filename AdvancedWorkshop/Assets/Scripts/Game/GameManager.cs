using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 lastCheckpointPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the last checkpoint position to the player's starting position.
        lastCheckpointPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpointPosition = checkpointPosition;

    }

    public void RespawnPlayer()
    {
        PlayerController.instance.SetCheckpoint(lastCheckpointPosition);
        PlayerController.instance.RespawnAtLastCheckpoint();
    }

}
