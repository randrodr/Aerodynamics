using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ara;

public class TrailsOnPath : MonoBehaviour
{
	public GameObject vehicle;
	public int vehiclesCount;
	public float speed;

	public float trailAlpha;

	List<GameObject> vehicleInstances = new List<GameObject>();
	public List<Vector3> path = new List<Vector3>();

	void Start ()
	{
		foreach (Transform child in transform)
		{
			if (child.GetComponent<Waypoint>())
			{
				path.Add(child.localPosition); 
			}
		}

		for (int i = 0; i < vehiclesCount; i++)
		{
			vehicleInstances.Add(Instantiate(vehicle, transform, false));
			vehicleInstances[i].transform.localPosition = Vector3.zero;
			//iTween.PutOnPath(vehicleInstances[i], path.ToArray(), 0f);
		}

		////vehicleInstances[0].transform.localPosition = path[0];
		//iTween.PutOnPath(vehicleInstances[0], path.ToArray(), 0f);
		//iTween.PutOnPath(vehicleInstances[1], path.ToArray(), 0f);

		StartCoroutine(MoveAlongPath());
	}

	IEnumerator MoveAlongPath()
	{
		for (int i = 0; i < vehicleInstances.Count; i++)
		{
			iTween.MoveTo(vehicleInstances[i], iTween.Hash("path", path.ToArray(), "movetopath", false, "time", speed, "islocal", true, "easetype", iTween.EaseType.linear, "looptype", "loop"));
			yield return new WaitForSeconds(speed / vehicleInstances.Count);
		}
	}
	
	private void Update()
	{
		//foreach (AraTrail trail in GetComponentsInChildren<AraTrail>())
		//{
		//	trail.initialColor.a = trailAlpha;
		//	Debug.Log("a trail is " + trail.initialColor);
		//}
	}
}
