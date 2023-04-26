using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFromVectors : MonoBehaviour
{
	LineRenderer line;

	public Transform[] points;

	Vector3[] vectors;

	// Use this for initialization
	void Start ()
	{
		if (!line)
		{
			line = GetComponent<LineRenderer>();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		vectors = new Vector3[points.Length];
		line.positionCount = points.Length;

		for (int i = 0; i < points.Length; i++)
		{
			vectors[i] = points[i].position;
		}

		line.SetPositions(vectors);
	}
}
