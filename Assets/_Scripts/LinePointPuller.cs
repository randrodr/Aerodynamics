using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class LinePointPuller : MonoBehaviour
{
	public LineRenderer lineToAffect;
	public float strength;
	public Easings.Functions easeType;

	SphereCollider col;
	Vector3 translation;

	private void Start()
	{
		col = GetComponent<SphereCollider>();
        translation = transform.localPosition;
	}

	private void Update()
	{
        
        translation = translation - transform.localPosition;

		for (int i = 0; i < lineToAffect.positionCount; i++)
		{
			float distance = Vector3.Distance(lineToAffect.GetPosition(i) + lineToAffect.transform.position, transform.position);
			if (distance < col.radius)
			{
				strength = CalculateStrength(distance);

				lineToAffect.SetPosition(i, lineToAffect.GetPosition(i) - translation * strength);
			}
		}

		translation = transform.localPosition;
	}

	private float CalculateStrength(float distance)
	{
		float strength = 1 - distance / col.radius;

		//string methodName = string.Concat("Easings.Functions.", easeType.ToString());
		//MethodInfo mi = GetType().GetMethod(methodName);
		//mi.Invoke(this, strength);		

		
		switch (easeType)
		{
			case Easings.Functions.Linear:
				return Easings.Linear(strength);
			case Easings.Functions.QuadraticEaseInOut:
				return Easings.QuadraticEaseInOut(strength);
			case Easings.Functions.CubicEaseInOut:
				return Easings.CubicEaseInOut(strength);
			case Easings.Functions.QuarticEaseInOut:
				return Easings.QuarticEaseInOut(strength);
			case Easings.Functions.QuinticEaseInOut:
				return Easings.QuinticEaseInOut(strength);
			case Easings.Functions.ExponentialEaseInOut:
				return Easings.ExponentialEaseInOut(strength);
			default:
				Debug.LogError("EaseType " + easeType + " not implemented");
				break;
		}
		return strength;
	}
}
