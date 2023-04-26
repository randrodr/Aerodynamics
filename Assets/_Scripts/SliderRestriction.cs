using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderRestriction : MonoBehaviour
{
	public float lowerLimit, upperLimit;
	public bool shouldRestrict;

	public bool ShouldRestrict { get; set; }

	Slider slider;

	void Start ()
	{
		if(!slider)
		{
			slider = GetComponent<Slider>();
		}
	}

	public void CheckForRestriction()
	{
		if (ShouldRestrict)
		{
			float value = slider.value;

			if (value < lowerLimit)
			{
				slider.value = lowerLimit;
			}
			if (value > upperLimit)
			{
				slider.value = upperLimit;
			} 
		}
	}

	public void DoRestrict()
	{

	}
}
