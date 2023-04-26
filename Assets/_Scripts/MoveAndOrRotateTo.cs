using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndOrRotateTo : MonoBehaviour
{
	public RotateType rotationType;
	[Tooltip("Only if RotateBy")]
	public Vector3 fullRotations;
	public float duration = 1f;
	public iTween.EaseType easeType = iTween.EaseType.easeInOutCubic;
	public Hashtable tweenHash;

	Transform pointA;
	//public Transform pointB;
	public Vector3 pointA_position;
	public Vector3 pointA_rotation;
	public Vector3 pointB_position;
	public Vector3 pointB_rotation;

	enum CurrentPosition { atPointA, atPointB }
	CurrentPosition currentPosition = CurrentPosition.atPointA;

	public enum RotateType { RotateTo, RotateBy }
	

	void Awake ()
	{
		pointA = gameObject.transform;
		pointA_position = pointA.localPosition;
		pointA_rotation = pointA.localEulerAngles;
		//print(gameObject.name + "\n" + pointA.localEulerAngles + "\n" + pointA.eulerAngles + "\n" + pointA_rotation);
		// iTween hash
		tweenHash = new Hashtable();
		tweenHash.Add("position", pointB_position);
		tweenHash.Add("rotation", pointB_rotation);
		// these vector3 values are for RotateBy
		tweenHash.Add("x", fullRotations.x);
		tweenHash.Add("y", fullRotations.y);
		tweenHash.Add("z", fullRotations.z);
		tweenHash.Add("time", duration);
		tweenHash.Add("easetype", easeType);
		tweenHash.Add("islocal", true);
	}

	public void SetPointB()
	{
		pointB_position = transform.localPosition;
		pointB_rotation = transform.localEulerAngles;
	}


	public void ClearPointB()
	{
		pointB_position = Vector3.zero;
		pointB_rotation = Vector3.zero;
	}
	
	public void MoveTo()
	{
		if (rotationType != RotateType.RotateBy)
		{
			iTween.MoveTo(gameObject, tweenHash);
		}
	}

	public void RotateTo()
	{
		switch (rotationType)
		{
			case RotateType.RotateTo:
				iTween.RotateTo(gameObject, tweenHash);
				break;
			case RotateType.RotateBy:
				iTween.RotateBy(gameObject, tweenHash);
				break;
			default:
				break;
		}
	}


	public void TogglePoints()
	{
		switch (currentPosition)
		{
			case CurrentPosition.atPointA:
				currentPosition = CurrentPosition.atPointB;
				tweenHash["position"] = pointA_position;
				tweenHash["rotation"] = pointA_rotation;
				break;
			case CurrentPosition.atPointB:
				currentPosition = CurrentPosition.atPointA;
				tweenHash["position"] = pointB_position;
				tweenHash["rotation"] = pointB_rotation;
				break;
			default:
				break;
		}
	}

}
