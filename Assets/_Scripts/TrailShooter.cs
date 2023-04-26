using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ara;

public class TrailShooter : MonoBehaviour
{
	public GameObject vehicle;
	public float speed;

	GameObject vehicleInstance;

	void Start ()
	{
		vehicleInstance = Instantiate(vehicle, transform);
		Shoot();
	}
	
	public void Shoot()
	{
		vehicleInstance.transform.localPosition = Vector3.zero;
		vehicleInstance.GetComponent<AraTrail>().emit = true;
		//vehicleInstance.SetActive(true);
		iTween.MoveTo(vehicleInstance, iTween.Hash("position", transform.GetChild(0).localPosition, "islocal", true, "speed", speed, "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject, "oncomplete", "PrepareForLoop"));
	}

	void PrepareForLoop()
	{
		vehicleInstance.GetComponent<AraTrail>().emit= false;
		//vehicleInstance.SetActive(false);
		Shoot();
	}
}
