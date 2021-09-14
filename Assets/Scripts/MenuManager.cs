using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Transform Player;
    public float Zoom = 16;

    [Header("Buttons")]
    public Button RecenterButton;
    public Button ShowARButton;
    public Button ShowMapButton;

    [Header("Worlds")]
    public GameObject MapWorld;
    public GameObject ARWorld;

    

    private AbstractMap mapManager;

    private bool inAR;

    void Start()
    {
        mapManager = FindObjectOfType<AbstractMap>();

        AssignButtons();

        return;
        inAR = true;
        ToggleAR();
    }

    private void AssignButtons()
    {
        RecenterButton.onClick.AddListener(RecenterPlayer);
        ShowARButton.onClick.AddListener(ToggleAR);
        ShowMapButton.onClick.AddListener(ToggleAR);
    }

    private void ToggleAR()
    {
        // Map
        MapWorld.SetActive(inAR);
        RecenterButton.gameObject.SetActive(inAR);

        // AR
        ARWorld.SetActive(!inAR);
        ShowMapButton.gameObject.SetActive(!inAR);


        inAR = !inAR;
    }

    public void RecenterPlayer()
    {
        mapManager.UpdateMap(mapManager.WorldToGeoPosition(Player.position), Zoom);
    }
}
