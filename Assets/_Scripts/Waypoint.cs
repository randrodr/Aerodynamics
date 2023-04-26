using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	public Color gizmoColor = Color.blue;
	public float sphereRadius = .2f;

	void OnDrawGizmos ()
	{
		Gizmos.color = gizmoColor;
		Gizmos.DrawSphere(transform.position, sphereRadius);
	}
}
