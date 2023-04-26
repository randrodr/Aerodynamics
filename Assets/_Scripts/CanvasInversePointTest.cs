using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInversePointTest : MonoBehaviour
{
	public GameObject tower;
	
	void Start ()
	{
		MapPoint();
	}
	
	void MapPoint()
	{
		Vector3 relativePoint = gameObject.GetComponent<RectTransform>().InverseTransformPoint(tower.GetComponent<RectTransform>().position);
		Debug.Log("point is at " + relativePoint);
	}
}
