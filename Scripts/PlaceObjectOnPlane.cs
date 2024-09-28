
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectOnPlane : MonoBehaviour
{        
    [SerializeField] GameObject addUICanvas;
    [SerializeField] GameObject editUICanvas;

    [SerializeField] ARRaycastManager raycaster;
    [SerializeField] GameObject placedPrefab;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void OnPlaceObject(InputValue inputValue)
    {
        if (addUICanvas.activeSelf)
        {            
            Vector2 touchPosition = inputValue.Get<Vector2>();
            PlaceObject(touchPosition);          
        }
    }

    void PlaceObject(Vector2 touchPosition)
    {
        if (raycaster.Raycast(touchPosition, hits,TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Quaternion additionalRotation = Quaternion.Euler(90, 0, 0);
            Quaternion finalRotation = hitPose.rotation * additionalRotation;
            Instantiate(placedPrefab, hitPose.position,
            finalRotation);            
        }
    }
}