using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System;

[Serializable]
public class Waypoint
{
    public string Title;
    [Geocode]
    public string Location;
    

    [HideInInspector]
    public Transform WaypointReference;
}
