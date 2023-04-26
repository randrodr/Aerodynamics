using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBar : MonoBehaviour
{
	public Transform openPosition, closedPosition;

	public void Maximize()
	{
		transform.position = openPosition.position;
	}

	public void Minimize()
	{
		transform.position = closedPosition.position;
	}
}
