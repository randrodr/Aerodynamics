using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLinePoint : MonoBehaviour
{
	public CatmullRomLine line;

	float newY;

	float t;
	Vector3 p0, p1, p2, p3;
	
	void Update ()
	{
		if(!line)
		{
			newY = line.transform.position.y;

			for (int i = 1; i < line.controlPointsList.Length - 1; i++)
			{

				if(transform.position.z <= line.controlPointsList[i].position.z && 
					transform.position.z >= line.controlPointsList[i+1].position.z)
				{
					p1 = line.controlPointsList[i].position;
					p0 = line.controlPointsList[i - 1].position;
					p2 = line.controlPointsList[i + 1].position;
					p3 = line.controlPointsList[i + 2].position;

					t = Mathf.InverseLerp(p1.z, p2.z, transform.position.z);
				}
				else
				{
					//Debug.Log(transform.parent.name + ", at " + transform.position + " is not between " + line.controlPointsList[i].position + " and " + line.controlPointsList[i + 1].position);
				}
			}

			//newY = transform.InverseTransformPoint(line.GetCatmullRomPosition(t, p0, p1, p2, p3)).y;
			newY = line.GetCatmullRomPosition(t, p0, p1, p2, p3).y;
			transform.position = new Vector3(transform.parent.position.x, newY, transform.parent.position.z);
			//transform.position = line.GetCatmullRomPosition(t, p0, p1, p2, p3);
		}
		if (line)
		{
			newY = line.transform.position.y;

			for (int i = 1; i < line.controlPointsList.Length - 1; i++)
			{

				if (transform.position.z <= line.controlPointsList[i].position.z &&
					transform.position.z >= line.controlPointsList[i + 1].position.z)
				{
					p1 = line.controlPointsList[i].position;
					p0 = line.controlPointsList[i - 1].position;
					p2 = line.controlPointsList[i + 1].position;
					p3 = line.controlPointsList[i + 2].position;

					t = Mathf.InverseLerp(p1.z, p2.z, transform.position.z);
				}
				else
				{
					//Debug.Log(transform.parent.name + ", at " + transform.position + " is not between " + line.controlPointsList[i].position + " and " + line.controlPointsList[i + 1].position);
				}
			}

			//newY = transform.InverseTransformPoint(line.GetCatmullRomPosition(t, p0, p1, p2, p3)).y;
			newY = line.GetCatmullRomPosition(t, p0, p1, p2, p3).y;
			transform.position = new Vector3(transform.parent.position.x, newY, transform.parent.position.z);
			//transform.localPosition = transform.TransformPoint(line.GetCatmullRomPosition(t, p0, p1, p2, p3));
		}
	}
}
