using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void CheckpointActive()
    {
        // Set this checkpoint as the active checkpoint.
        GameManager.Instance.SetCheckpoint(transform.position);
        activated = true;
        spriteRenderer.sprite = sprites[1];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("checkpoint triggered");
        if (collision.CompareTag("Player") && !activated)
        {
            CheckpointActive();
        }
    }
}