using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PlacementWithManySelectionController : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomePanel;
    [SerializeField]
    private PlacementObject[] placedObjects;
    [SerializeField]
    private Color activeColor = Color.red;
    [SerializeField]
    private Color inactiveColor = Color.gray;
    [SerializeField]
    private Button dismissButton;
    [SerializeField]
    private Camera arCamera;


    private Vector2 touchPosition = default;
    //private ARRaycastManager arRaycastManager;
    //private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        //arRaycastManager = GetComponent<ARRaycastManager>();
        dismissButton.onClick.AddListener(Dismiss);
        ChangeSelectedObject(placedObjects[0]);
    }

    private void Dismiss() => welcomePanel.SetActive(false);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // do not capture events unless the welcome panel is hidden
        if (welcomePanel.activeSelf)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            
            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if(Physics.Raycast(ray, out hitObject))
                {
                    PlacementObject placementObject = hitObject.transform.GetComponent<PlacementObject>();
                    if (placementObject != null)
                    {
                        ChangeSelectedObject(placementObject);
                    }
                }
            }
        }
    }

    private void ChangeSelectedObject(PlacementObject selected)
    {
        foreach(PlacementObject current in placedObjects)
        {
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            if (selected != current)
            {
                current.IsSelected = false;
                meshRenderer.material.color = inactiveColor;
            }
            else
            {
                current.IsSelected = true;
                meshRenderer.material.color = activeColor;
            }
        }
    }
}
