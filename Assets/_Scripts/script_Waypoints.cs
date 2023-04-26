using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_Waypoints : MonoBehaviour {
    [HideInInspector]
    public Transform[] waypoints;//assigned by waypoint spawnner
    public float moveSpeed; //assigned in game manager for all waypoints
	public float rotateSpeed;
    [HideInInspector]//assigned per spawnner gameobject to override default speed
    public int currentWaypoint;
	
    private float distance;
	public float distanceThreshold;

	public GameObject mySpawner;
    //private List<GameObject> waypointList;

    // Update is called once per frame
    void Start()
    {
		transform.LookAt(waypoints[currentWaypoint]);
    }

    void Update ()
    {
        WaypointHandler();
	}

    void WaypointHandler()
    {
        if (currentWaypoint < waypoints.Length)
        {
            distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position);
        }
        if(distance > distanceThreshold)
        {
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

			Vector3 targetDir = waypoints[currentWaypoint].position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(targetDir);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }
        else  if (distance < distanceThreshold && currentWaypoint < waypoints.Length - 1)
        {
            currentWaypoint++;
        }
    }

    void OnDisable()
    {
        //mySpawner.GetComponent<script_WaypointSpawnner>().SpawnWaypoints();
    }
}
