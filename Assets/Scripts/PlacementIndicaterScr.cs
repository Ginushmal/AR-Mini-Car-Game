using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicaterScr : MonoBehaviour
{

    private ARRaycastManager raycastManager;
    private GameObject placementIndicator;


    // Start is called before the first frame update
    void Start()
    {
        // get the components
        raycastManager = FindObjectOfType<ARRaycastManager>();
        placementIndicator = transform.GetChild(0).gameObject;

        // hide the placement indicator
        placementIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // if we hit an AR plane, update the position and rotation of the placement indicator
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!placementIndicator.activeInHierarchy)
            {
                placementIndicator.SetActive(true);
            }
        }
    }
}
