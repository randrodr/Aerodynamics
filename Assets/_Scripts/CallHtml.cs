using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CallHtml : MonoBehaviour {

	[DllImport("__Internal")]
	private static extern void LoadPage(string page);
	private bool flipMenu = true;
	public GameObject DropDownMenu;

	public GameObject dropDownArrow, dropUpArrow;

	public void OpenDropDown(){

		DropDownMenu.SetActive(flipMenu);
		flipMenu = !flipMenu;

		ToggleGameobjects();
	}

	void ToggleGameobjects()
	{
		dropDownArrow.SetActive(flipMenu);
		dropUpArrow.SetActive(!flipMenu);
	}

	public void LoadPageHtml(string page)
	{
		Debug.Log("Loading this page: " + page);
		#if UNITY_WEBGL && !UNITY_EDITOR
			LoadPage(page);
		#endif
	}
}
