using MPDepthCore.TrackingSources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackingSourceDisplay : MonoBehaviour
{
    [SerializeField] MPDepthTrackingSource trackingSource;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }
    private void Update()
    {
        text.text = trackingSource.name + ": (" + trackingSource.TrackingData.CameraTrackingData.Position.x + ", " + trackingSource.TrackingData.CameraTrackingData.Position.y + ", " + trackingSource.TrackingData.CameraTrackingData.Position.z + ")";
    }
}
