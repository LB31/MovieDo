using KDTree;
using Mapbox.CheapRulerCs;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public float DistanceForInteraction = 10;

    public Waypoint CurrentGoal;

    private DirectionsFactory directions;
    private ILocationProvider locationProvider;

    private AbstractMap map;
    private List<Vector2d> wpLoactions = new List<Vector2d>();

    public double[] testCords;

    private void Start()
    {
        directions = GetComponent<DirectionsFactory>();
        locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
        map = LocationProviderFactory.Instance.mapManager;

        //CurrentGoal = FindNearestWaypoint();
        //SelectWaypoint();
    }

    [ContextMenu("SelectWaypoint")]
    public void SelectWaypoint()
    {
        // test TODO
        directions._waypoints[1] = CurrentGoal.WaypointReference;

        StartCoroutine(CalcDistanceToGoal()); // TODO stop when reached
    }

    [ContextMenu("Find nearest WP")]
    public Waypoint FindNearestWaypoint()
    {
        // Player position
        Vector2d playerPos = locationProvider.CurrentLocation.LatitudeLongitude;

        // Get nearest waypoint
        double nearest = Mathd.Infinity;
        Waypoint nearestWP = null;
        foreach (var wp in WaypointManager.Instance.Waypoints)
        {
            Vector2d wpLocation = Conversions.StringToLatLon(wp.Location);
            double distance = CalculateDistance(playerPos, wpLocation);
            if (distance < nearest)
            {
                nearest = distance;
                nearestWP = wp;
            }

        }

        return nearestWP;
    }

    private IEnumerator CalcDistanceToGoal()
    {
        double distance = Mathf.Infinity;
        while (distance > DistanceForInteraction)
        {
            Vector2d playerPos = locationProvider.CurrentLocation.LatitudeLongitude;
            Vector2d curGoal = Conversions.StringToLatLon(CurrentGoal.Location);
            distance = CalculateDistance(playerPos, curGoal);
            yield return new WaitForSeconds(3);
            //print(distance + " distance");
        }
        // Goal was reached
        // TODO show start AR Button
 
    }

    private double CalculateDistance(Vector2d start, Vector2d goal)
    {
        double[] startFormat = new double[] { start[0], start[1] };
        double[] goalFormat = new double[] { goal[0], goal[1] };
        CheapRuler cheapRuler = new CheapRuler(startFormat[1], CheapRulerUnits.Meters);
        double distance = cheapRuler.Distance(startFormat, goalFormat);
        return distance;
    }

}
