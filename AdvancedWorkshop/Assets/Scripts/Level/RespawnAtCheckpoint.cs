using UnityEngine;

public class RespawnAtCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.RespawnPlayer();
        }
    }
}
