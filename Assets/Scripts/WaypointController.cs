using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System;

public class WaypointController : MonoBehaviour
{
    public ARPlanePlacer ARPlanePlacer;
    public Waypoint Waypoint;

    private void OnMouseDown()
    {
        Debug.Log("clicked wp " + Waypoint.WaypointReference.name);

        GameManager.Instance.CurrentContent = Waypoint.SceneContent;
        FindObjectOfType<Navigator>().CurrentGoal = Waypoint;
    }

}



