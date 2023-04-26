using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ara;

public class TrailColorDirection : MonoBehaviour
{
	AraTrail trail;
	Vector3 lastPos;

	public Color upDirection;
	public Color downDirection;

	public Renderer rend;
	
	void Start ()
	{
		trail = GetComponent<AraTrail>();
	}
	
	void Update ()
	{
		if(transform.localPosition.y > lastPos.y)
		{
			trail.initialColor = upDirection;
		}
		else
		{
			trail.initialColor = downDirection;
		}

		lastPos = transform.localPosition;
	}
}
