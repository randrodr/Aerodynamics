using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour {

    #region //SingletonSetup
    public static WaypointsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public float moveSpeed;
    public float rotateSpeed;
    public float minDistance = 0.4f;//distance needed to consider it being close enough to target waypoint //Change as needed. Good starting suggestion is placedwen

    public float spawnTimeInterval;//Spawn time in seconds when the next waypoint should spawn
}
