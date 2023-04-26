using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTween : MonoBehaviour
{
	LineRenderer line;

	public int tweenablePoint;

	void Start ()
	{
		if(line == null)
		{
			line = GetComponent<LineRenderer>();
		}

		// -- test --
		TweenPoint(Vector3.right, 2f);
	}

	public void TweenPoint(Vector3 toPos, float time)
	{
		//Vector3 fromPos = new Vector3(line.GetPosition(tweenablePoint).x, line.GetPosition(tweenablePoint).y, line.GetPosition(tweenablePoint).z);
		Vector3 fromPos = line.GetPosition(tweenablePoint);
		Vector3 newPos = new Vector3();

		iTween.ValueTo(gameObject, iTween.Hash(
			"from", fromPos,
			"to", toPos,
			"time", time,
			"onupdatetarget", gameObject,
			"onupdateparams", newPos,
			"onupdate", "UpdatePoint"
			));
	}

	public void TweenPoint(Vector3 toPos)
	{
		float calculatedTime = 2f;



		TweenPoint(toPos, calculatedTime);
	}

	public void UpdatePoint(Vector3 newPos)
	{
		line.SetPosition(tweenablePoint, newPos);
	}

	Vector3 MapPoint(Vector3 pointPos)
	{
		Vector3 newPos = new Vector3();

		return newPos;
	}
}
