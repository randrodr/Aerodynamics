using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; //Goes on object you would like the event to happen in

/// <summary>
/// Scroll bar API link https://docs.unity3d.com/ScriptReference/UI.Scrollbar.html
/// </summary>

public class script_Controls : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler //required per event action
{
    private GameManager GameManager;

    private GameObject helicopter;
    private Scrollbar helicopterHeight;
    private float heightMultiplier;

    //scroll bar temp variables
    private float currentInducedFlowValue;
    private float currentHeightScrollValue;
    private float newHeight;

    private bool keyToggled = true;
    public GameObject rotorKey;
    private GameObject crossSection;
    private GameObject collective;
    private GameObject crossSectionDiagramHandler;
    private GameObject crossSectionDiagramWaypoint;
    private GameObject crossSectionInducedFlowHandler;

    private float crossSectionMaxAngle;
    private float crossSectionInducedAngle;
    private float crossSectionInitialRotationZ;
    private float collectiveMoveMultiplier;// scroll bar max value of 1 times amount
    private float crossSectionDiagramMinSize;
    private float crossSectionDiagramMaxSize;

    //Waypoint speed for induced flow
    public float inducedFlowWaypointSpeedMin;//mathf lerp references
    public float inducedFlowWaypointSpeedMax;
    private float actualInducedFlowSpeed;

    //controls the radius size of waypoints using scale
    //public GameObject topWaypointSpawnner;
    //public GameObject bottomWaypointSpawnner;
    //public float minWaypointScale; //mathf lerp references
    //public float maxWaypointScale;
    //private float actualWaypointScale;

    /// Notes for smooth lerp
    /// Make a float for that to equal the new height vector when you move the scrollbar.
    /// Make the helicopter smoothdamp towards that specific location afterwards.

    void Awake()
    {
        //Game Manager self assigned variables
        GameManager = GameManager.instance;
        helicopter = GameManager.helicopter;
        crossSection = GameManager.crossSection;
        crossSectionDiagramHandler = GameManager.crossSectionDiagramHandler;
        crossSectionDiagramWaypoint = GameManager.crossSectionDiagramWaypoint;
        collective = GameManager.collective;
        crossSectionDiagramMinSize = GameManager.crossSectionDiagramMinSize;
        crossSectionDiagramMaxSize = GameManager.crossSectionDiagramMaxSize;
        crossSectionInducedFlowHandler = GameManager.crossSectionInducedFlowHandler;

        crossSectionMaxAngle = GameManager.crossSectionMaxAngle;
        crossSectionInducedAngle = GameManager.crossSectionInducedAngle;
        heightMultiplier = GameManager.helicopterHeightMultiplier;
        crossSectionInitialRotationZ = GameManager.crossSectionInitialRotationZ;
        collectiveMoveMultiplier = GameManager.collectiveMoveMultiplier;
}

    void Start ()//Initial values for all variables
    {
        ///When the game starts the scroll bar is in the middle position but the respective objects are in respect to the scroll bar
        ///This forces all objects to take their inital position
        //topWaypointSpawnner.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        //bottomWaypointSpawnner.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        GameManager.inducedFlowHandle.GetComponent<Slider>().value = 0f;
        GameManager.heightHandle.GetComponent<Slider>().value = 0f;
        currentHeightScrollValue = 0f;
        currentInducedFlowValue = 0f;
        newHeight = currentHeightScrollValue;
        AdjustHeight(0f);//Sets initial pos of cross section as well as collective
        AdjustInducedFlow(0f);//means zero since its backwards
        //waypoint default speed assigned in inspector
    }

    public void AdjustHeight(float height)
    {
        ///Height is the current position of the scroll bar which holds a value of 0 min 1 max
        ///Adjust height is controlled by Unity Slider GUI in inspector
        ///The height float is the only way to access the scroll bar variable 0-1
        ///the height is subtracted by the induced flow so that induced flow affects height
        ///the height multiplier makes the helicopter go up and down at heigher rates. Otherwise it would be between 0 and 1
        currentHeightScrollValue = height;
        heightCalculation();
        changeCrossSection();
        WaypointChanges();

    }

    public void AdjustInducedFlow(float intensity)
    {
        currentInducedFlowValue = intensity * 0.25f; //lowering inducedflow by by 75%
        heightCalculation();
        changeCrossSection();

        //removed waypoint functionality
        #region
        //WaypointChanges();

        //since the intensity scroll bar is upside down it goes from 1-0 instead of 0-1, solved the induced flow speed by subtracting by 1 and you get the opposite number.
        //float inducedFlowSpeed = intensity;//Game manager induced flow speed
        //inducedFlowSpeed = Mathf.Abs(inducedFlowSpeed);//this takes the induced flow number subtracted and makes it positive so that the remainder convers between 0-1 and not a negative number.
        //actualInducedFlowSpeed = Mathf.Lerp(inducedFlowWaypointSpeedMin, inducedFlowWaypointSpeedMax, inducedFlowSpeed); //lerps between min and max numbers using the 0-1 slider values

        //do the same lerp for scale. Scale of waypoint spawn area changes based on induced flow amount
        //actualWaypointScale = Mathf.Lerp(minWaypointScale, maxWaypointScale, inducedFlowSpeed);
        //topWaypointSpawnner.transform.localScale = new Vector3(actualWaypointScale,actualWaypointScale,actualWaypointScale);
        //bottomWaypointSpawnner.transform.localScale = new Vector3(actualWaypointScale, actualWaypointScale, actualWaypointScale);

        //GameManager.waypointMoveSpeed = actualInducedFlowSpeed;
        #endregion
    }

    void heightCalculation()//and move helicopter
    {
        newHeight = (currentInducedFlowValue + currentHeightScrollValue); //+2 so its not fighting with ground when zero
        //newHeight = currentHeightScrollValue * currentInducedFlowValue;
        helicopter.transform.position = new Vector3(helicopter.transform.position.x, newHeight * heightMultiplier +2, helicopter.transform.position.z);

    }

    void changeCrossSection()
    {
        ///cross section angle never exceeds 18
        ///We will add 18 if scroll bar is at 1 and subtract 18 if scroll bar at 0
        float crossSectionInducedNewAngle = crossSectionInducedAngle * currentInducedFlowValue;
        float crossSectionNewAngle = crossSectionMaxAngle * currentHeightScrollValue;
        crossSectionInducedFlowHandler.transform.localEulerAngles = new Vector3(0f, 0f, crossSectionInducedNewAngle);
        crossSection.transform.eulerAngles = new Vector3(-11.075f, 0f, crossSectionInitialRotationZ + crossSectionNewAngle); //For some reason I have to manually set rotations otherwise they auto set to zero if I use transform.rot.x/y
        collective.transform.eulerAngles = new Vector3(180 + crossSectionNewAngle * collectiveMoveMultiplier, -90, collective.transform.rotation.z);

        float currentCrossSectionDiagramValue = Mathf.Lerp(crossSectionDiagramMinSize, crossSectionDiagramMaxSize, currentInducedFlowValue);
        crossSectionDiagramHandler.transform.localScale = new Vector3(crossSectionDiagramHandler.transform.localScale.x, currentCrossSectionDiagramValue, crossSectionDiagramHandler.transform.localScale.z);
        crossSectionDiagramWaypoint.transform.localScale = new Vector3(crossSectionDiagramWaypoint.transform.localScale.x, currentCrossSectionDiagramValue, crossSectionDiagramWaypoint.transform.localScale.z);
    }

    public void ToggleKey()
    {
        if (keyToggled) { rotorKey.SetActive(false); keyToggled = false; }
        else { rotorKey.SetActive(true); keyToggled = true; }
    }

    void WaypointChanges()
    {
        for(int i = 0; i < GameManager.waypointList.Count; i++)
        {
            if(GameManager.waypointList[i].GetComponent<script_Waypoints>().currentWaypoint == 0)
            {
                //GameManager.waypointList[i].GetComponent<script_Waypoints>().currentWaypoint++;
            }
        }
    }

    //These scripts require a specific signature in order to work with event system/ ALL REFERENCE
    public void OnDrag(PointerEventData pointerEventData)//moves the blades/cross section also
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log("Release");

    }
}
