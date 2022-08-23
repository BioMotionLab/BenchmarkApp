using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MPDepthCore.Calibration.Camera;
using OffAxisCamera;
using StreamTrackingSystem;
using MPDepthCore;

public class DataOutput : MonoBehaviour
{
    [SerializeField] MotiveRawTrackingSource motiveSource;
    [SerializeField] TrackingSystemsManager streamSource;
    [SerializeField] StreamTrackingDataReceiver streamCameraData;
    [SerializeField] int numberOfSamples;
    [SerializeField] Transform calibrationOffset;
    [SerializeField] TrackingCalibrationManager trackingManager;
    bool beginWrite;
    public int i;
    int frameCount;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        StreamWriter writer = File.AppendText("C:/Users/BiomotionLab/Desktop/output/" + "Output.csv");
        /*writer.WriteLine("Time" + "," + "Motive X (Screen)" + "," + "Motive Y (Screen)" + "," + "Motive Z (Screen)" + "," + "AR X (Screen)" + "," + "AR Y (Screen)" + "," + "AR Z (Screen)"
            + "," + "Motive X (Camera)" + "," + "Motive Y (Camera)" + "," + "Motive Z (Camera)" + "," + "AR X (Camera)" + "," + "AR Y (Camera)" + "," + "AR Z (Camera)" + "," +
            "Motive Orientation X (Raw)" + "," + "Motive Orientation Y (Raw)" + "," + "Motive Orientation Z (Raw)" + "," + "ARKit Orientation X (Screen)" + "," + "ARKit Orientation Y (Screen)" + "," + "ARKit Orientation Z (Screen)" + "," +
            "ARKit Orientation (Camera)" + "," + "ARKit Orientation Y (Camera)" + "," + "ARKit Orientation Z (Camera)");*/
        writer.WriteLine("Time" + "," + 
            "Motive Head Location X" + "," + "Motive Head Location Y" + "," + "Motive Head Location Z" + "," 
            + "Motive Head Orientation X" + "," + "Motive Head Orientation Y" + "," + "Motive Head Orientation Z" + "," 
            + "ARKit Head Location X" + "," + "ARKit Head Location Y" + "," + "ARKit Head Location Z" + "," 
            + "ARKit Head Orientation X" + "," + "ARKit Head Orientation Y" + "," + "ARKit Head Orientation Z" + ","
            + "Motive Screen Location X" + "," + "Motive Screen Location Y" + "," + "Motive Screen Location Z" + ","
            + "Motive Screen Orientation X" + "," + "Motive Screen Orientation Y" + "," + "Motive Screen Orientation Z" + ","
            + "Motive iPhone Location X" + "," + "Motive iPhone Location Y" + "," + "Motive iPhone Location Z" + "," 
            + "Motive iPhone Orientation X" + "," + "Motive iPhone Orientation Y" + "," + "Motive iPhone Orientation Z" + "," 
            + "Calibration Translation X" + "," + "Calibration Translation Y" + "," + "Calibration Translation Z" + ","
            + "Calibration Rotation X" + "," + "Calibration Rotation Y" + "," + "Calibration Rotation Z" + ","
            + "ARKit Head Location (Calibrated) X" + "," + "ARKit Head Location (Calibrated) Y" + "," + "ARKit Head Location (Calibrated) Z" + "," 
            +"ARKit Head Rotation (Calibrated) X" + "," + "ARKit Head Rotation (Calibrated) Y" + "," + "ARKit Head Rotation (Calibrated) Z");
        writer.Close();
        frameCount = 60 / numberOfSamples;

    }

    private void Update()
    {
        WriteToFile();


    }

    private void WriteToFile()
    {
        if (beginWrite)
        {
            string time = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + ":" + System.DateTime.Now.Second.ToString();
            Vector3 motiveCameraPosition = motiveSource.cameraObject.transform.position;
            Vector3 motiveCameraRotation = motiveSource.cameraObject.transform.rotation.eulerAngles ;
            Vector3 ARKitRawPosition;
            Vector3 ARKitRawRotation;
            Vector3 motiveScreenPosition = motiveSource.screenObject.transform.position;
            Vector3 motiveScreenRotation = motiveSource.screenObject.transform.rotation.eulerAngles;
            Vector3 motivePhonePosition = motiveSource.phoneObject.transform.position ;
            Vector3 motivePhoneRotation = motiveSource.phoneObject.transform.rotation.eulerAngles;
            Vector3 calibrationPosition = calibrationOffset.position;
            Vector3 calibrationRotation = calibrationOffset.rotation.eulerAngles;
            Vector3 calibratedPosition;
            Vector3 calibratedRotation;

            if (streamCameraData.ReceivedData.IsTracking)
            {
                ARKitRawPosition = streamCameraData.ReceivedData.CameraTrackingData.Position;
                ARKitRawRotation = streamCameraData.ReceivedData.CameraTrackingData.Eulers;
                calibratedPosition = streamSource.CurrentCalibratedTrackingData.CameraTrackingData.Position;
                calibratedRotation = streamSource.CurrentCalibratedTrackingData.CameraTrackingData.Eulers;
            }

            else
            {
                ARKitRawPosition = new Vector3(-999, -999, -999);
                ARKitRawRotation = new Vector3(-999, -999, -999);
                calibratedPosition = new Vector3(-999, -999, -999);
                calibratedRotation = new Vector3(-999, -999, -999);
            }



            StreamWriter writer = File.AppendText("C:/Users/BiomotionLab/Desktop/output/" + "Output.csv");


            writer.WriteLine(time + "," +
                motiveCameraPosition.x + "," +
                motiveCameraPosition.y + "," +
                motiveCameraPosition.z + "," +
                motiveCameraRotation.x + "," +
                motiveCameraRotation.y + "," +
                motiveCameraRotation.z + "," +
                ARKitRawPosition.x + "," +
                ARKitRawPosition.y + "," +
                ARKitRawPosition.z + "," +
                ARKitRawRotation.x + "," +
                ARKitRawRotation.y + "," +
                ARKitRawRotation.z + "," +
                motiveScreenPosition.x + "," +
                motiveScreenPosition.y + "," +
                motiveScreenPosition.z + "," +
                motiveScreenRotation.x + "," +
                motiveScreenRotation.y + "," +
                motiveScreenRotation.z + "," +
                motivePhonePosition.x + "," +
                motivePhonePosition.y + "," +
                motivePhonePosition.z + "," +
                motivePhoneRotation.x + "," +
                motivePhoneRotation.y + "," +
                motivePhoneRotation.z + "," +
                calibrationPosition.x + "," +
                calibrationPosition.y + "," +
                calibrationPosition.z + "," +
                calibrationRotation.x + "," +
                calibrationRotation.y + "," +
                calibrationRotation.z + "," +
                calibratedPosition.x + "," +
                calibratedPosition.y + "," +
                calibratedPosition.z + "," +
                calibratedRotation.x + "," +
                calibratedRotation.y + "," +
                calibratedRotation.z) ;



            writer.Close();
        }

    }

    public string GetTime()
    {
        return System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + ":" + System.DateTime.Now.Second.ToString() + ":" + System.DateTime.Now.Millisecond.ToString() + ": ";
    }

    public void BeginOutput()
    {
        beginWrite = true;
        i = 0;
    }

}
