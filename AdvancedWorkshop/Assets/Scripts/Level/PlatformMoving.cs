using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float checkDistance = 0.05f;


    private Transform targetWaypoint;
    private int currentWaypoitIndex = 0;

    void Awake()
    {
        targetWaypoint = waypoints[0];
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);


        if (Vector2.Distance(transform.position, targetWaypoint.position) < checkDistance)
        {
            targetWaypoint = GetNextWaypoint();
        }
    }

    private Transform GetNextWaypoint()
    {
        currentWaypoitIndex++;
        if (currentWaypoitIndex >= waypoints.Length)
        {
            currentWaypoitIndex = 0;
        }
        return waypoints[currentWaypoitIndex];
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(10))
        {
            other.transform.parent.parent = transform;
            Debug.Log("Player on the vertical moving platform.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(10))
        {
            other.transform.parent.parent = null;
        }
    }
}
