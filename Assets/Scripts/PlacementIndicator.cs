using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    private ARPlanePlacer aRPlanePlacer;

    private void OnEnable()
    {
        aRPlanePlacer = FindObjectOfType<ARPlanePlacer>();
    }

    private void OnMouseDown()
    {
        aRPlanePlacer.PlaceObject();
    }
}
