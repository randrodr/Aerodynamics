using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
	Text textComponent;

	public int minValue, maxValue;
	public Color slowColor, fastColor;

	void Start ()
	{
		textComponent = GetComponent<Text>();
		
	}
	
	void ChangeText(string newText)
	{
		textComponent.text = newText;
	}

	public void MapValues(float percent)
	{
		int newValue = 0;

		newValue = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, percent / 100));

		ChangeText(newValue.ToString());
	}

	public void MapColors(float percent)
	{
		textComponent.color = Color.Lerp(slowColor, fastColor, percent / 100);
	}
}
