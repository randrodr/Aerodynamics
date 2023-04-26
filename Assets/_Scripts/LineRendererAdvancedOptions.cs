using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererAdvancedOptions : MonoBehaviour {

    [Header("LineRenderer Parameters for unity 2017x")]

    [Tooltip("Assign Line Renderer Object")]
    public LineRenderer LineRendererObject;

    [Header("Parameters :")]

    [Tooltip("Old Parameter Options")]
    public float StartWidth = 0f;
    public float EndWidth = 0f;

    public Color StartColor;
    public Color Endcolor;

    public bool UseWorldSpace;

	void Start () {
        StartWidth = LineRendererObject.startWidth;
        EndWidth = LineRendererObject.endWidth;

        StartColor = LineRendererObject.startColor;
        Endcolor = LineRendererObject.endColor;

        UseWorldSpace = LineRendererObject.useWorldSpace;
    }
	
}
