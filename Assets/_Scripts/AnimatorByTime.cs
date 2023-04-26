using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorByTime : MonoBehaviour
{
	public string stateName;

	Animator animator;
	int layerIndex;

	public float startFrame;

	void Start()
	{
		animator = GetComponent<Animator>();

		layerIndex = animator.GetLayerIndex(stateName);
		PlayAnimFromFrame(startFrame);
	}

    public void PlayAnimFromTime (float normalizedTime)
	{
		animator.Play(stateName, -1, normalizedTime);
	}

	public void PlayAnimFromCheckpoint(RectTransform rectTransform)
	{
		float normalizedTime = rectTransform.anchoredPosition.x / rectTransform.parent.GetComponent<RectTransform>().rect.width;
		
		PlayAnimFromTime(normalizedTime);
	}

	public void PlayAnimFromFrame(float frame)
	{
		int frameNumber = (int)frame;
		float totalFrames = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * 60;
		if(frameNumber >= totalFrames)
		{
			frameNumber -= 1;
		}
		animator.Play(stateName, layerIndex, frameNumber / totalFrames);
	}

	public void TogglePlayback()
	{
		if (animator.speed != 0)
		{
			animator.speed = 0; 
		}
		else
		{
			animator.speed = 1;
		}
	}

    
}
