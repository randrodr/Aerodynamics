using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
	public int materialIndex;
	public float directionInDegrees;
	public float speedInKnots;

	Material material;
	Vector2 scrollVector;
	Vector2 tileScaleFactor;
	
	void Start ()
	{
		material = GetComponent<MeshRenderer>().materials[materialIndex];
		tileScaleFactor = new Vector2(transform.localScale.x / material.mainTextureScale.x, transform.localScale.y / material.mainTextureScale.y);
	}
	
	void LateUpdate ()
	{
		scrollVector = CalculateScrollOffset(directionInDegrees) * CalculateMetersPerSecond(speedInKnots);
		material.mainTextureOffset += (scrollVector / tileScaleFactor) * Time.deltaTime ;
		material.mainTextureOffset = new Vector2(material.mainTextureOffset.x % 1, material.mainTextureOffset.y % 1);
	}

	Vector2 CalculateScrollOffset(float angleInDegrees)
	{
		return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	float CalculateMetersPerSecond(float knots)
	{
		return knots * .514444f;
	}

	public void ChangeSpeed(float newSpeed)
	{

	}

	public float Speed
	{
		set
		{
			speedInKnots = value;
		}
	}
}
