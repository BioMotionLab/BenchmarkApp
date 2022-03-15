using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPDepthCore.Calibration.Screen;
using UnityEngine;



    public class RotatableDeviceScreen : ScreenCalibrationProvider
    {

        DeviceOrientation DeviceOrientation => Input.deviceOrientation;

        float DefaultWidth => currentCalibration.PortraitModeHeight;
        float DefaultHeight => currentCalibration.PortraitModeWidth;

        [SerializeField] RotatableScreenCalibrator screenCalibrator = default;
    [SerializeField] GameObject calibrationUI;

        public override float Width
        {
            get
            {
                switch (DeviceOrientation)
                {

                    case DeviceOrientation.Portrait:
                    case DeviceOrientation.PortraitUpsideDown:
                        return currentCalibration.PortraitModeWidth;

                    case DeviceOrientation.LandscapeLeft:
                    case DeviceOrientation.LandscapeRight:
                        return currentCalibration.PortraitModeHeight;

                    default:
                        return DefaultWidth;
                }
            }
        }

        public override float Height
        {
            get
            {
                switch (DeviceOrientation)
                {

                    case DeviceOrientation.Portrait:
                    case DeviceOrientation.PortraitUpsideDown:
                        return currentCalibration.PortraitModeHeight;

                    case DeviceOrientation.LandscapeLeft:
                    case DeviceOrientation.LandscapeRight:
                        return currentCalibration.PortraitModeWidth;

                    default:
                        return DefaultHeight;
                }
            }
        }

        

    public override void Calibrate()
    {
        // calibrationUI.SetActive(true);
    }

    public override void SelectCalibration(int selectedIndex)
        {
            currentCalibration = defaultCalibrations[selectedIndex].screenCalibration;
            calibrationUI.SetActive(false);
        }

        public override SavedScreenCalibration CurrentCalibration => currentCalibration;

        public override List<SavedScreenCalibration> AllCalibrations =>
            new List<SavedScreenCalibration>(allCalibrations);

        [SerializeField] public SavedRotatableScreenCalibration currentCalibration = default;

        [SerializeField] List<SavedRotatableScreenCalibration> savedCalibrations = new List<SavedRotatableScreenCalibration>();

        [SerializeField] List<RotatableDeviceConfig> defaultCalibrations = new List<RotatableDeviceConfig>();

        [SerializeField] List<SavedRotatableScreenCalibration> allCalibrations;

        protected override string Filename => "SavedRotatableScreenConfigurations.json";


        [Serializable]
        public class SavedRotatableScreenCalibration : SavedScreenCalibration
        {

            [SerializeField] public string name = "Default Name";

            [SerializeField] public float PortraitModeHeight = default;
            [SerializeField] public float PortraitModeWidth = default;

            public string Name
            {
                get => name;
                set => name = value;
            }
        }

        [Serializable]
        public class SaveData
        {
            [SerializeField] public SavedRotatableScreenCalibration currentCalibration;

            [SerializeField] public List<SavedRotatableScreenCalibration> savedCalibrations;

            public SaveData(List<SavedRotatableScreenCalibration> savedCalibrations, SavedRotatableScreenCalibration currentCalibration)
            {
                this.savedCalibrations = savedCalibrations;
                this.currentCalibration = currentCalibration;
            }

        }
    }
