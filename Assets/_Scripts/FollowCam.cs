using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
	public Transform transformToFollow;
	public Vector3 offset;
	public bool useEditorOffset;

	void Start ()
	{
		if(useEditorOffset)
		{
			offset = transform.position - transformToFollow.position;
		}
	}
	
	void LateUpdate()
	{
		transform.position = transformToFollow.position + offset;
	}

	void SetOffset(Vector3 newOffset)
	{
		offset = newOffset;
	}
}
