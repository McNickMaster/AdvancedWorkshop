using UnityEngine;

public class VerticalPlatformMovement : MonoBehaviour
{
    public float speed = 5f;            // Speed at which the platform moves vertically.
    public float topBoundary = 5f;      // Top boundary of movement.
    public float bottomBoundary = -5f;  // Bottom boundary of movement.

    private bool movingUp = true;       // Flag to track movement direction.

    private void Update()
    {
        // Calculate the platform's new position.
        Vector3 newPosition = transform.position;

        // Update the position based on the movement direction and speed.
        if (movingUp)
        {
            newPosition.y += speed * Time.deltaTime;
            if (newPosition.y > topBoundary)
            {
                newPosition.y = topBoundary;
                movingUp = false;
            }
        }
        else
        {
            newPosition.y -= speed * Time.deltaTime;
            if (newPosition.y < bottomBoundary)
            {
                newPosition.y = bottomBoundary;
                movingUp = true;
            }
        }

        // Apply the new position to the platform.
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
            Debug.Log("Player on the vertical moving platform.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
