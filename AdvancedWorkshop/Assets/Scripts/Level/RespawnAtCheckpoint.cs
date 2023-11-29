using UnityEngine;

public class RespawnAtCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(10))
        {
            GameManager.Instance.RespawnPlayer();
        }
    }
}
