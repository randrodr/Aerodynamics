using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

//Render line between two points
public class script_LineRenderer : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public Transform origin;
    public Transform destination;
    public float startWidth;
    public float endWidth;
    public Color lineColor;

    void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start ()
    {
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, destination.position);
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
	}
	
	// Update is called once per frame
	void Update ()
    {
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, destination.position);
    }
}
