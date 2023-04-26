using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Header("All GameObjects")]
    public GameObject helicopter;
    public GameObject crossSection;
    public GameObject collective;
    public GameObject inducedFlowHandle;
    public GameObject heightHandle;
    public GameObject crossSectionDiagramHandler; //to scale the two arrows
    public GameObject crossSectionDiagramWaypoint;//to handle the yellow waypoint so it moves up and down scale wise
    public GameObject crossSectionInducedFlowHandler;
    public List<GameObject> waypointList;
    [Space(15)]
    [Header("Cross Section")]
    public float collectiveMoveMultiplier;
    public float crossSectionInitialRotationZ;
    public float crossSectionMaxAngle;
    public float crossSectionInducedAngle;
    public float crossSectionDiagramMinSize;
    public float crossSectionDiagramMaxSize;
    public float helicopterHeightMultiplier;
    [Space(15)]
    [Header ("Induced Flow")]
    public float inducedFlowStartTime;
    public GameObject[] inducedFlowWaypoints;
    public float waypointMoveSpeed;//speed for waypoints
	public float waypointRotateSpeed;
	public float waypointSpawnInterval;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        //StartCoroutine("enableInducedFlow");
    }
	

    IEnumerator enableInducedFlow()
    {
        yield return new WaitForSeconds(inducedFlowStartTime);
        foreach (GameObject i in inducedFlowWaypoints)
        {
            i.SetActive(true);
        }
    }

	public void PauseGame()
	{
		if (Time.timeScale != 0)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}
