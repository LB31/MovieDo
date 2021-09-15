using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

public class WaypointManager : Singleton<WaypointManager>
{
    public GameObject WaypointPrefab;
    public Transform WaypointParent;
    public List<Waypoint> Waypoints;

    private AbstractMap map;
    private List<Vector2d> wpLoactions = new List<Vector2d>();

    private void Start()
    {
        map = LocationProviderFactory.Instance.mapManager;

        PlaceWaypoints();
    }

    public void PlaceWaypoints()
    {
        wpLoactions = new List<Vector2d>();
        map = LocationProviderFactory.Instance.mapManager;

        foreach (var wp in Waypoints)
        {
            Vector2d location = Conversions.StringToLatLon(wp.Location);
            Transform instance = Instantiate(WaypointPrefab, WaypointParent).transform;
            wp.WaypointReference = instance;
            instance.name = wp.Title;
            instance.position = map.GeoToWorldPosition(location, true);
            instance.gameObject.AddComponent<WaypointController>().Waypoint = wp;

            wpLoactions.Add(location);
        }
    }

    private void Update()
    {
        for (int i = 0; i < Waypoints.Count; i++)
        {
            Waypoints[i].WaypointReference.position = map.GeoToWorldPosition(wpLoactions[i], true);
            //SpawnedPoIs[i].localScale = new Vector3(SpawnScale, SpawnScale, SpawnScale);
        }
    }
}

[Serializable]
public class Waypoint
{
    public string Title;
    [Geocode]
    public string Location;

    public GameObject SceneContent;

    [HideInInspector]
    public Transform WaypointReference;
}