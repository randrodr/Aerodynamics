using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineBar : MonoBehaviour
{
	Image image;
	public Animator animator;
	public string animState;

	private void Start()
	{
		image = GetComponent<Image>();
	}

	void Update ()
	{
		image.fillAmount = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1; // Mod 1 because animation normalized time goes past 1 on loops
	}
}
