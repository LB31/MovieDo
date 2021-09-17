using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


/// <summary>
/// Moves the ARSessionOrigin in such a way that it makes the given content appear to be
/// at a given location acquired via a raycast.
/// </summary>
[RequireComponent(typeof(ARSessionOrigin))]
[RequireComponent(typeof(ARRaycastManager))]
public class ARPlanePlacer : MonoBehaviour
{
    // Debug
    public bool Debugging;

    public GameObject ContentRepresentation;
    public Transform Content;

    public GameObject PlaceUI;

    [HideInInspector]
    public bool PlacementAllowed { get; set; } = true;
    //public GameObject RecenterButton;

    private ARSessionOrigin m_SessionOrigin;
    private ARRaycastManager m_RaycastManager;

    [SerializeField]
    [Tooltip("The rotation the content should appear to have.")]
    Quaternion m_Rotation;

    private bool placementPoseIsValid;
    private Pose placementPose;


    private void Start()
    {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
        m_RaycastManager = GetComponent<ARRaycastManager>();

        ContentRepresentation.SetActive(true);
        if (Content)
            Content.gameObject.SetActive(false);

        //RecenterButton.SetActive(false);
    }

    private void OnEnable()
    {
        Content = GameManager.Instance.CurrentContent.transform;
        TooglePlacing(true);

        PlaceUI.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(Content.gameObject);
    }


    void Update()
    {
        // Return when placement was paused
        if (!PlacementAllowed || !gameObject.activeSelf) return;

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    // https://github.com/Unity-Technologies/arfoundation-samples/issues/25
    private bool IsPointOverAnyObject(Vector2 pos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(pos.x, pos.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0 || Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, 1 << 3);

    }

    // Currently called by PlaceButton Button Object
    public void PlaceObject()
    {
        if (!placementPoseIsValid) return;

        Content = Instantiate(Content).transform;

        float angle = Quaternion.Angle(Content.rotation, m_SessionOrigin.transform.rotation);
        placementPose.rotation.y = angle;
        m_SessionOrigin.MakeContentAppearAt(Content, placementPose.position, Quaternion.Euler(0, Camera.main.transform.rotation.y, 0));

        TooglePlacing(false);
    }

    // Currently called by RecenterWorldButton Button Object
    public void TooglePlacing(bool startPlacing)
    {
        //RecenterButton.SetActive(!startPlacing);
        Content.gameObject.SetActive(!startPlacing);
        ContentRepresentation.SetActive(startPlacing); // Test !
        PlacementAllowed = startPlacing;
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            ContentRepresentation.SetActive(true);
            PlaceUI.SetActive(false);

            //ContentRepresentation.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            ContentRepresentation.transform.position = placementPose.position;
        }
        else
        {
            ContentRepresentation.gameObject.SetActive(false);
            PlaceUI.SetActive(true);
        }
    }

    private void UpdatePlacementPose()
    {
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        //arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);
        if (m_RaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            placementPoseIsValid = hits.Count > 0;
            placementPose = hits[0].pose;
        }

        if (placementPoseIsValid)
        {
            // let object look in same z direction as camera
            //Vector3 newRotation = ContentRepresentation.transform.eulerAngles;
            //newRotation.y = Camera.main.transform.eulerAngles.y;
            //ContentRepresentation.transform.eulerAngles = newRotation;
            //return;

            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }


}
