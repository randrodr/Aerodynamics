using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(MoveAndOrRotateTo))]
[CanEditMultipleObjects]

public class TransformSetter : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		MoveAndOrRotateTo moveScript = (MoveAndOrRotateTo)target;


		if (GUILayout.Button("Set Point B"))
		{
			moveScript.SetPointB();
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}


		//if (GUILayout.Button("Clear Point B"))
		//{
		//	moveScript.ClearPointB();
		//	EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		//}
	}

	void OnDrawGizmosSelected()
	{

	}
}
