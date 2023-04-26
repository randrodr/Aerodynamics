using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierLineRenderer))]
[CanEditMultipleObjects]
public class BezierLineRenderer_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		BezierLineRenderer lineScript = (BezierLineRenderer)target;

		if(GUILayout.Button("Create Line"))
		{
			lineScript.SetLinePoints();
		}
		//if (GUILayout.Button("Move Point"))
		//{
		//	lineScript.MoveKeys();
		//}
	}


	// Update is called once per frame
	void Update () {
		
	}
}
