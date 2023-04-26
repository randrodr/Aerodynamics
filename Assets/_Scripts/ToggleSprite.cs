using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSprite : MonoBehaviour
{
	Image image;

	public Sprite startingSprite;
	public Sprite otherState;

	void Start ()
	{
		image = GetComponent<Image>();
		image.sprite = startingSprite;
	}

	public void Toggle()
	{
		if(image.sprite != otherState)
		{
			image.sprite = otherState;
		}
		else
		{
			image.sprite = startingSprite;
		}
	}
}
