using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 lastCheckpointPosition;

    public InputAction pauseAction;
    public GameObject pauseMenu;

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

        pauseAction.performed += context => Pause();
    }

    private void Start()
    {
        // Initialize the last checkpoint position to the player's starting position.
        lastCheckpointPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void Update()
    {

    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
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

    public void OnEnable()
    {
        pauseAction.Enable();
    }

}
