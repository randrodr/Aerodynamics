using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTextTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Color highlightedColor;

	Text buttonText;
	Color normalColor;

	void Awake()
	{
		if(!buttonText)
		{
			buttonText = GetComponentInChildren<Text>();
		}
		if(buttonText)
		{
			normalColor = buttonText.color;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(buttonText)
		{
			buttonText.color = highlightedColor;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (buttonText)
		{
			buttonText.color = normalColor;
		}
	}
}
