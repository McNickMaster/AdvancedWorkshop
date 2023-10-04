using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed = 5f;         // Speed at which the platform moves.
    public float leftBoundary = -5f; // Left boundary of movement.
    public float rightBoundary = 5f; // Right boundary of movement.

    private bool movingRight = true; // Flag to track movement direction.

    private void Update()
    {
        // Calculate the platform's new position.
        Vector3 newPosition = transform.position;

        // Update the position based on the movement direction and speed.
        if (movingRight)
        {
            newPosition.x += speed * Time.deltaTime;
            if (newPosition.x > rightBoundary)
            {
                newPosition.x = rightBoundary;
                movingRight = false;
            }
        }
        else
        {
            newPosition.x -= speed * Time.deltaTime;
            if (newPosition.x < leftBoundary)
            {
                newPosition.x = leftBoundary;
                movingRight = true;
            }
        }

        // Apply the new position to the platform.
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
if (other.tag=="Player")
            {
            other.transform.parent.SetParent(transform);
            Debug.Log("moving platform");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            other.transform.parent.SetParent(null);
        }
    }
    private void FixedUpdate()
    {
        
    }
}
