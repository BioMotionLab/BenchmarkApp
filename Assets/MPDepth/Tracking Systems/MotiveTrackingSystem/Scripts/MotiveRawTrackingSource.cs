using System;
using System.Collections;
using System.Collections.Generic;
using MPDepthCore.Calibration.Camera;
using MPDepthCore.TrackingSources;
using UnityEngine;

public class MotiveRawTrackingSource : MPDepthTrackingSource
{
    [SerializeField] public GameObject cameraObject = default;

    [SerializeField] public GameObject screenObject = default;
    [SerializeField] public GameObject phoneObject;


    public Vector3 GetCameraToScreenPosition;
    public Vector3 positionInCameraCoords;
    public Vector3 rotationInScreenCoords;
    public Vector3 rotationInCameraCoords;

    public override void TurnOff()
    {

    }

    public override void TurnOn()
    {

    }

    private void Update()
    {
        positionInCameraCoords = cameraObject.transform.position - phoneObject.transform.position;
        GetCameraToScreenPosition = cameraObject.transform.position - screenObject.transform.position;
        rotationInCameraCoords = cameraObject.transform.rotation.eulerAngles - phoneObject.transform.rotation.eulerAngles;
        rotationInScreenCoords = cameraObject.transform.rotation.eulerAngles - screenObject.transform.rotation.eulerAngles;
        CameraTrackingData cameraTrackingData = new CameraTrackingData
        {
            Position = GetCameraToScreenPosition
        };


        MPDepthTrackingData data = new RawTrackingData
        {
            CameraTrackingData = cameraTrackingData,
            IsTracking = true
        };
        TrackingDataUpdated?.Invoke(data);
    }

    public override event TrackingDataUpdatedEvent TrackingDataUpdated;
}
