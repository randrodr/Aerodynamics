using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Formation))]
public class Formation_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Formation formationScript = (Formation)target;

		if (GUILayout.Button("Generate Formation"))
		{
			formationScript.FormUp(formationScript.dimensions);
		}
	}
}
