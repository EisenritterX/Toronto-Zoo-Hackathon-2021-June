using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class PlacementController : MonoBehaviour
{

    //[SerializeField]private GameObject placedPrefab;

    //public GameObject PlacedPrefab
    //{
    //    get
    //    {
    //        return placedPrefab;
    //    }
    //    set
    //    {
    //        placedPrefab = value;
    //    }
    //}

    // AR
    private ARRaycastManager arRaycastManager;
    [SerializeField] Camera arCamera;

    // Orangutan
    [SerializeField] private GameObject orangPrefab;

    // Orangutan
    private GameObject orang;

    // UIPanel Detection
    [SerializeField] private GameObject uiPanel;

    // PlaneManager
    [SerializeField] private ARPlaneManager arPlaneManager;

    

    
    // Time stamp before applying
    private float timeStamp;
    [SerializeField]
    private float cooldownPeriodInSeconds = 3.0f;


    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        timeStamp = Time.time;
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null&& orang == null)
        {
            ARPlane arPlane = args.added[0];
            orang = Instantiate(orangPrefab, arPlane.transform.position, orangPrefab.transform.rotation);
        }
    }

    //bool TryGetTouchPosition(out Vector2 touchPosition)
    //{
    //    if(Input.touchCount > 0){
    //        touchPosition = Input.GetTouch(0).position;
    //        return true;
    //    }
    //    touchPosition = default;
    //    return false;
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                var touchPosition = touch.position;
                bool isOverUI = touchPosition.IsPointOverUIObject();

                if (isOverUI)
                {
                    return;
                }

                if (!isOverUI&&arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && Time.time > timeStamp)
                {
                    var hitPose = hits[0].pose;

                    Instantiate(DataHandler.Instance.prop, hitPose.position, DataHandler.Instance.prop.transform.rotation);
                    timeStamp = Time.time + cooldownPeriodInSeconds;
                }
            }
        }

    }

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
