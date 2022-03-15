using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MPDepthCore.Calibration.Camera;

public class DataOutput : MonoBehaviour
{
    [SerializeField] MotiveRawTrackingSource motiveSource;
    [SerializeField] TrackingCalibrationManager streamSource;
    float t;
    bool canWrite;
    int i;

    private void Awake()
    {
        StreamWriter writer = File.AppendText("C:/Users/BML_Demo_Computer/Desktop/output/" + "Output.csv");
        writer.WriteLine("Time" + "," + "Motive X" + "," + "Motive Y" + "," + "Motive Z" + "," + "AR X" + "," + "AR Y" + "," + "AR Z");
        writer.Close();
        canWrite = true;
        t = 0;
    }

    private void Update()
    {

        if (canWrite)
        {
            WriteToFile();
            i++;
        }
        
        if(i >= 60)
        {
            canWrite = false;
        }
        t += Time.deltaTime;
        if(t >= 1.0f)
        {
            canWrite = true;
            i = 0;
            t = 0;
        }

    }

    private void WriteToFile()
    {
        StreamWriter writer = File.AppendText("C:/Users/BML_Demo_Computer/Desktop/output/" + "Output.csv");
        writer.WriteLine(Time.time + "," + motiveSource.TrackingData.CameraTrackingData.Position.x + "," +
             motiveSource.TrackingData.CameraTrackingData.Position.y + "," +
             motiveSource.TrackingData.CameraTrackingData.Position.z + "," +
             streamSource.calibratedTrackingData.CameraTrackingData.Position.x + "," +
             streamSource.calibratedTrackingData.CameraTrackingData.Position.y + "," +
             streamSource.calibratedTrackingData.CameraTrackingData.Position.z);
        writer.Close();

    }

}
