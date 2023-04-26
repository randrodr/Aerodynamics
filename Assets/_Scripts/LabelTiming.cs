using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelTiming : MonoBehaviour
{
	public Image timelineFill;
	public List<RectTransform> checkpoints;
	public List<GameObject> labels;

	public Dictionary<float, GameObject> checkpointLabels = new Dictionary<float, GameObject>();

	void Start ()
	{
		for(int i = 0; i < checkpoints.Count; i++)
		{
			float normalizedTime = checkpoints[i].anchoredPosition.x / timelineFill.rectTransform.rect.width;
			checkpointLabels.Add(normalizedTime, labels[i]);
			//Debug.Log(checkpointLabels.Keys);
		}
	}
	
	void Update ()
	{
		foreach (float time in checkpointLabels.Keys)
		{
			if ((int)(timelineFill.fillAmount * 100) == (int)(time * 100))
			{
				DisplayLabel(checkpointLabels[time]);
			}
		}
	}

	void DisplayLabel(GameObject labelToDisplay)
	{
		foreach(GameObject label in labels)
		{
			label.SetActive(false);
		}

		labelToDisplay.SetActive(true);
	}
}
