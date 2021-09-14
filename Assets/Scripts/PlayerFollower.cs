using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform WhoToFollow;
    public bool Follow = true;
    public float Zoom = 16;

    private AbstractMap mapManager;
    private float originY;

    void Start()
    {
        mapManager = FindObjectOfType<AbstractMap>();
        originY = transform.position.y;
    }

    void Update()
    {
        if (Follow)
            transform.position = new Vector3(WhoToFollow.position.x, transform.position.y, WhoToFollow.position.z);
    }

    public void Recenter()
    {
        mapManager.UpdateMap(mapManager.WorldToGeoPosition(WhoToFollow.position), Zoom);
    }
}
