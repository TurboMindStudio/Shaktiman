using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuktukAi : MonoBehaviour
{
    public Transform[] waypoints; // Array to store the waypoints
    public float speed = 3f; // Speed of movement between waypoints
    public float rotationSpeed = 5f; // Speed of rotation
    private int currentWaypointIndex = 0; // Current waypoint index
    public Transform[] wheels;
    public float WheelRotatespeed;
    void Update()
    {
        if (waypoints.Length == 0)
            return;

        // Move towards the current waypoint
        Transform currentWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);

        // Rotate to face the current waypoint
        Vector3 direction = currentWaypoint.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Check if the AI has reached the current waypoint
        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }


        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].Rotate(Vector3.right * WheelRotatespeed * Time.deltaTime);
        }
    }
}
