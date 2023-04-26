using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierLineRenderer : MonoBehaviour
{
	LineRenderer lineRenderer;

	public Vector3 pointA;
	public Vector3 pointB;

	public Transform pointBTransform;

	public int resolution;

	public float moveSpeed = .2f;

	[HideInInspector]
	public float flowSpeed;

	public bool setupForAnimation;
	public Color slowColor, fastColor;

	int middleKey;

	

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();

		if (!setupForAnimation)
			return;

		AddGradientPoint(.4f, slowColor);
		AddGradientPoint(.5f, slowColor);
		AddGradientPoint(.6f, slowColor);

		for (int i = 0; i < lineRenderer.colorGradient.colorKeys.Length; i++)
		{
			if (lineRenderer.colorGradient.colorKeys[i].time > .49f && lineRenderer.colorGradient.colorKeys[i].time < .51f)
			{
				middleKey = i;
			}
		}
	}
	
	public void SetLinePoints()
	{
		SetLinePoints(pointB);
	}

	public void SetLinePoints(Vector3 newPointB)
	{
		if (!lineRenderer)
		{
			lineRenderer = GetComponent<LineRenderer>();
		}
		lineRenderer.positionCount = resolution + 2;

		// interpolation

		for (int i = 0; i < lineRenderer.positionCount; i++)
		{
			Vector3 newPos = new Vector3();

			if (i == 0)
			{
				newPos = pointA;
			}
			else
			{
				float lerpPercent = i / (float)(lineRenderer.positionCount - 1);
				newPos = Vector3.Lerp(pointA, newPointB, lerpPercent);
			}

			lineRenderer.SetPosition(i, newPos);
			//Debug.Log("linerenderer " + newPos);
		}
	}

	void AddGradientPoint(float location, Color color)
	{
		Gradient gradient = lineRenderer.colorGradient;

		List<GradientColorKey> newList = new List<GradientColorKey>(lineRenderer.colorGradient.colorKeys);		
		newList.Add(new GradientColorKey(color, location));

		gradient.SetKeys(newList.ToArray(), lineRenderer.colorGradient.alphaKeys);
		lineRenderer.colorGradient = gradient;
	}

	private void Update()
	{
		TestKeyMove();
		//AddGradientPoint(.5f, MapFloatToGradient(flowSpeed));

		if(pointBTransform)
		{
			SetLinePoints(pointBTransform.localPosition);
		}
	}

	public void MapFloatToGradient(float percent)
	{
		Gradient gradient = lineRenderer.colorGradient;
		Color color = Color.Lerp(slowColor, fastColor, (percent / 100f));

		List <GradientColorKey> newList = new List<GradientColorKey>(lineRenderer.colorGradient.colorKeys);
		newList.RemoveAt(middleKey);
		newList.Add(new GradientColorKey(color, .5f));

		gradient.SetKeys(newList.ToArray(), lineRenderer.colorGradient.alphaKeys);
		lineRenderer.colorGradient = gradient;
	}

    public void MapFloatToCustomPosGradient(float percent, float gradientPos)
    {
        Gradient gradient = lineRenderer.colorGradient;
        Color color = Color.Lerp(slowColor, fastColor, (percent / 100f));

        List<GradientColorKey> newList = new List<GradientColorKey>(lineRenderer.colorGradient.colorKeys);
        newList.RemoveAt(middleKey);
        newList.Add(new GradientColorKey(color, gradientPos));

        gradient.SetKeys(newList.ToArray(), lineRenderer.colorGradient.alphaKeys);
        lineRenderer.colorGradient = gradient;
    }

    void TestKeyMove()
	{
		List<Keyframe> newFrames = new List<Keyframe>();
		foreach(Keyframe thisFrame in lineRenderer.widthCurve.keys)
		{
			newFrames.Add(new Keyframe(thisFrame.time - moveSpeed * Time.deltaTime, thisFrame.value, thisFrame.inTangent, thisFrame.outTangent, thisFrame.inWeight, thisFrame.outWeight));
			
		}
		if (moveSpeed > 0 && newFrames[1].time <= 0)
		{
			newFrames.Add(new Keyframe(1, newFrames[0].value, newFrames[0].inTangent, newFrames[0].outTangent, newFrames[0].inWeight, newFrames[0].outWeight));
			newFrames.Remove(newFrames[0]);
		}
		else if(moveSpeed < 0 && newFrames[1].time >= 1)
		{
			newFrames.Add(new Keyframe(0, newFrames[newFrames.Count-1].value, newFrames[newFrames.Count - 1].inTangent, newFrames[newFrames.Count - 1].outTangent, newFrames[newFrames.Count - 1].inWeight, newFrames[newFrames.Count - 1].outWeight));
			newFrames.Remove(newFrames[newFrames.Count - 1]);
		}
		AnimationCurve lineCurve = new AnimationCurve(newFrames.ToArray());
		lineRenderer.widthCurve = lineCurve;
	}
}
