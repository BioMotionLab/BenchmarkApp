using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MPDepthCore.Calibration.Camera;
using OffAxisCamera;
using StreamTrackingSystem;
using MPDepthCore;

public class LiveARStream : MonoBehaviour
{

    [SerializeField] StreamTrackingDataReceiver streamCameraData;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(streamCameraData.ReceivedData.CameraTrackingData.Position.x, streamCameraData.ReceivedData.CameraTrackingData.Position.y, streamCameraData.ReceivedData.CameraTrackingData.Position.z);
        transform.eulerAngles = new Vector3(streamCameraData.ReceivedData.CameraTrackingData.Eulers.x, streamCameraData.ReceivedData.CameraTrackingData.Eulers.y, streamCameraData.ReceivedData.CameraTrackingData.Eulers.z);


    }
}
