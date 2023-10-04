using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("checkpoint triggered");
        if (collision.CompareTag("Player") && !activated)
        {
            // Set this checkpoint as the active checkpoint.
            GameManager.Instance.SetCheckpoint(transform.position);
            activated = true;
        }
    }
}