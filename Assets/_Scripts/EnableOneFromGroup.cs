using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOneFromGroup : MonoBehaviour
{
	public List<GameObject> objectGroup;

	public void EnableJust(GameObject go)
	{
		foreach(GameObject groupMember in objectGroup)
		{
			groupMember.SetActive(false);
		}

		go.SetActive(true);
	}

	public void EnableByIndex(int goIndex)
	{
		Debug.Log("Should enable " + objectGroup[goIndex]);
		EnableJust(objectGroup[goIndex]);
	}
	
}
