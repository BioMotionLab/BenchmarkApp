using MPDepthCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHead : MonoBehaviour
{
    [SerializeField] TrackingSystemsManager trackingSystemsManager;
    [SerializeField] Matrix4x4 m;
    void Update()
    {
        transform.position = trackingSystemsManager.CurrentCalibratedTrackingData.CameraTrackingData.Position;
        transform.rotation = Quaternion.Euler(trackingSystemsManager.CurrentCalibratedTrackingData.CameraTrackingData.Eulers);
        m = Matrix4x4.Rotate(transform.rotation);
    }
}
