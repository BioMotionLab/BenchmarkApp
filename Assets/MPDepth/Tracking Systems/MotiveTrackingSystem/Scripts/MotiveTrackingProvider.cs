using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPDepthCore.Calibration.Camera;
using UnityEngine;

public class MotiveTrackingProvider : TrackingCalibrationProvider
{
    [SerializeField]
    SavedStreamTrackingConfiguration currentCalibration;

    [SerializeField] List<SavedStreamTrackingConfiguration> savedCalibrations;

    [SerializeField] MotiveTrackingCalibration calibrator;

    
    public override void SelectCalibration(int selectedIndex)
    {
        currentCalibration = savedCalibrations[selectedIndex];
    }

    public override void Calibrate()
    {
        calibrator.CalculateCalibrationFromTrackingInfo();
    }

    public override TrackerOffsetCalibration GetTrackerOffsetCalibration =>
        currentCalibration.TrackerOffsetCalibration;

    public override SavedTrackerCalibration CurrentTrackerCalibration => currentCalibration;
    public override List<SavedTrackerCalibration> AllCalibrations => new List<SavedTrackerCalibration>(savedCalibrations);

    protected override string Filename => "SavedStreamTrackerConfigurations.json";


    [Serializable]
    public class SavedStreamTrackingConfiguration : SavedTrackerCalibration
    {

        public string name = "Default Name";

        public TrackerOffsetCalibration TrackerOffsetCalibration = new TrackerOffsetCalibration();

        public string Name
        {
            get => name;
            set => name = value;
        }
    }

    [Serializable]
    public class SaveData
    {

        [SerializeField]
        public List<SavedStreamTrackingConfiguration> savedConfigurations;

        [SerializeField]
        public SavedStreamTrackingConfiguration currentCalibration;

        public SaveData(List<SavedStreamTrackingConfiguration> savedConfigurations, SavedStreamTrackingConfiguration currentCalibration)
        {
            this.savedConfigurations = savedConfigurations;
            this.currentCalibration = currentCalibration;
        }

    }



}
