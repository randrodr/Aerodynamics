using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSpawner : MonoBehaviour {

    private Transform travelerSpawnPoint;

    public GameObject travelingObject;//Object travling through waypoints
    public Transform[] myWaypoints; //Place waypoint locations here

    [Header("Change these values to override Manager Values")]
    public float spawnTimeInterval;//Timer based spawn point. How long before a new spawn?
    public float moveSpeed;
    public float rotateSpeed;
    public float minDistance;

    private bool coRoutineRunning = false;

	public Color pathColor;

    void Start()
    {
        travelerSpawnPoint = transform;//spawnpoint is where the GameObject is with this script
        if (spawnTimeInterval == 0) { spawnTimeInterval = WaypointsManager.instance.spawnTimeInterval; } //assign spawn timer from Game Manager

            StartCoroutine("SpawnerDelayOnEnable");
            //SpawnWaypoints over time
    }

    void OnEnable()
    {
         StartCoroutine("SpawnerDelayOnEnable");
         //this ensures the coroutine is started anytime the GameObject is enabled
    }

    IEnumerator SpawnerDelayOnEnable()
    {
        //a check to make sure that this doesnt run twice when starting for the first time
        yield return new WaitForSeconds(spawnTimeInterval);
        if (!coRoutineRunning)
        {
            coRoutineRunning = true;
            {
                StartCoroutine("TravelerSpawner");
            }
        }
    }

    IEnumerator TravelerSpawner()
    {
        while (true)
        {
            //immediately spawn an object
            GameObject traveler = Instantiate(travelingObject, travelerSpawnPoint.transform.position, travelerSpawnPoint.transform.rotation, travelerSpawnPoint); //instantiate prefab named newWaypoint on myself
            traveler.GetComponent<WaypointsTraveler>().waypointLocations = myWaypoints; //pass my locations or waypoints to the child traveling object

			//List<Vector3> tweenPath = new List<Vector3>() { };

			//for (int i = 0; i < myWaypoints.Length; i++)
			//{
			//	tweenPath.Add(myWaypoints[i].position);
			//}

			//traveler.transform.LookAt(traveler.GetComponent<WaypointsTraveler>().waypointLocations[0]);

			if (moveSpeed == 0) { moveSpeed = WaypointsManager.instance.moveSpeed; }
            if (rotateSpeed == 0) { rotateSpeed = WaypointsManager.instance.rotateSpeed; }
            if (minDistance == 0) { minDistance = WaypointsManager.instance.minDistance; }
            if (spawnTimeInterval == 0) { spawnTimeInterval = WaypointsManager.instance.minDistance; }

            traveler.GetComponent<WaypointsTraveler>().moveSpeed = moveSpeed;
            traveler.GetComponent<WaypointsTraveler>().rotateSpeed = rotateSpeed;
            traveler.GetComponent<WaypointsTraveler>().minDistance = minDistance;

            traveler.SetActive(true);
			//iTween.MoveTo(traveler, iTween.Hash("path", tweenPath.ToArray(), "speed", moveSpeed, "easetype", iTween.EaseType.easeInOutSine));

			yield return new WaitForSeconds(spawnTimeInterval);//seconds before next waypoint spawns
        }
    }

	private void OnDrawGizmosSelected()
	{
		if(myWaypoints.Length > 0)
		{
			iTween.DrawPath(myWaypoints, pathColor);
		}
	}

	void OnDisable()
    {
        coRoutineRunning = false;
    }
}
