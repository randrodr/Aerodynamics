using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_WaypointSpawnner : MonoBehaviour
{
    public GameObject waypointPrefab;
    private Transform waypointSpawn;
	public float spawnInterval = 5;
	public float moveSpeed;
	public float rotateSpeed;
	public Transform[] myWaypoints;
    private Vector3 myPosition;
    private GameObject exisitingWaypoint;

	List<GameObject> waypointList;

	void Awake()
    {
        waypointSpawn = transform;
		//spawnInterval = GameManager.instance.waypointSpawnInterval;
	}

    // Use this for initialization
    void Start()
    {
        StartCoroutine("waypointSpawnner");
        //SpawnWaypoints();
    }

	void OnDrawGizmos()
	{
		for(int i = 0; i < myWaypoints.Length; i++)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(myWaypoints[i].position, .2f);
			Gizmos.color = Color.yellow;
			if (i < myWaypoints.Length - 1)
			{
				Gizmos.DrawLine(myWaypoints[i].position, myWaypoints[i + 1].position);
			}
		}		
	}

	IEnumerator waypointSpawnner()
    {
        while (true)
        {
            GameObject newWaypoint = Instantiate(waypointPrefab, waypointSpawn.transform.position, waypointSpawn.transform.rotation);
            newWaypoint.GetComponent<script_Waypoints>().waypoints = myWaypoints;

			if(moveSpeed == 0)
			{
				newWaypoint.GetComponent<script_Waypoints>().moveSpeed = 5;//GameManager.instance.waypointMoveSpeed;
			}
			else
			{
				newWaypoint.GetComponent<script_Waypoints>().moveSpeed = moveSpeed;
			}
			if(rotateSpeed == 0)
			{
				newWaypoint.GetComponent<script_Waypoints>().rotateSpeed = 5;// GameManager.instance.waypointRotateSpeed;
			}
			else
			{
				newWaypoint.GetComponent<script_Waypoints>().rotateSpeed = rotateSpeed;
			}

			newWaypoint.SetActive(true);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnAndAddToList()//List Pool Spawn REF NO LONGER USED
    {
        while (true)
        {
            myPosition = transform.position;
            GameObject newWaypoint = Instantiate(waypointPrefab, waypointSpawn.transform.position, waypointSpawn.transform.rotation);
            waypointList.Add(newWaypoint);
            newWaypoint.transform.parent = null;
            newWaypoint.transform.position = myPosition;
            newWaypoint.GetComponent<script_Waypoints>().waypoints = myWaypoints;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnWaypoints()
    {

        myPosition = transform.position;
        GameObject newWaypoint = Instantiate(waypointPrefab, waypointSpawn.transform.position, waypointSpawn.transform.rotation);
        newWaypoint.GetComponent<script_Waypoints>().waypoints = myWaypoints;
        newWaypoint.GetComponent<script_Waypoints>().mySpawner = gameObject;
        newWaypoint.transform.position = myPosition;
        newWaypoint.transform.parent = null;
    }
}
