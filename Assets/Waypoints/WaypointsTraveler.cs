using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ara; //Needs Ara Trails to work. If you dont want to use Ara Trials, just hide this namespace or delete it

public class WaypointsTraveler : MonoBehaviour
{

    [HideInInspector] //All set by the Waypoint Manager or Spawner
    public float moveSpeed;
    [HideInInspector]
    public float rotateSpeed;
    [HideInInspector]
    public float minDistance;

    [HideInInspector]
    public Transform[] waypointLocations;

    private int currentWaypoint;
    private float distanceFromWaypoint;

	public iTweenPath tweenPath;

    void Update()
    {
        MoveWaypoint();		
    }

    void Start()
    {
        if (moveSpeed == 0) { moveSpeed = WaypointsManager.instance.moveSpeed; }
        if (rotateSpeed == 0) { rotateSpeed = WaypointsManager.instance.rotateSpeed; }
        if (minDistance == 0) { minDistance = WaypointsManager.instance.minDistance; }
	}

    void MoveWaypoint()
    {
		//tweenPath.nodes = new List<Vector3>();
		//for (int i = 0; i < waypointLocations.Length; i++)
		//{
		//	tweenPath.nodes.Add(waypointLocations[i].position);
		//}
		//iTween.MoveTo(gameObject, iTween.Hash("path", tweenPath.nodes.ToArray(), "speed", 2, "easetype", iTween.EaseType.easeInOutSine));

		if (currentWaypoint < waypointLocations.Length)//If I am not at the last waypoint... keep moving
        {
            distanceFromWaypoint = Vector3.Distance(transform.position, waypointLocations[currentWaypoint].transform.position);//calculate distance from current waypoint
        }
        if (distanceFromWaypoint > minDistance)//If I am far from waypoint move and rotate me
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);//move towards waypoint location


            Vector3 targetDir = waypointLocations[currentWaypoint].position - transform.position;//calculating my forward position in respect to waypoint position
            Quaternion rotation = Quaternion.LookRotation(targetDir);//turning vector to quaternion that
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);//using quaternion to rotate over time. This moves you in arcs instead of straight lines
        }
        else if (distanceFromWaypoint < minDistance && currentWaypoint < waypointLocations.Length - 1) //if I am close enough to waypoint and not at my last waypoint on list
        {
            currentWaypoint++;//same as i++ in a for loop, focus on the next waypoint
        }

        else if (distanceFromWaypoint < minDistance && currentWaypoint == waypointLocations.Length - 1)
        {
            StartCoroutine("WaitForDestroy");
        }
    }

    IEnumerator WaitForDestroy()
    {
        if (gameObject.GetComponent<AraTrail>() != null)
        {
            yield return new WaitForSeconds(gameObject.GetComponent<AraTrail>().time);
        }
        //if not using ara trails, just put in whatever time you want, this waits for the tail on the
        //trail renderer tail to reach zero instead of deleting it as it is still appearing to be travling
        else { yield return new WaitForSeconds(0); }
        Destroy(gameObject);
    }

}
