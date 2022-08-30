using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using MPDepthCore.Calibration.Camera;
using MPDepthCore.TrackingSources;
using OffAxisCamera;
using UnityEngine;
using UnityEngine.UI;

namespace MPDepthCore
{
    public class TrackingSystemsManager : MonoBehaviour
    {
        int currentTrackingSystemIndex = 0;
        public static TrackingSystemsManager instance;

        [SerializeField] OffAxisCameraRig offAxisCameraRig = default;

        [SerializeField] TrackingSystem currentTrackingSystem;

        [SerializeField] List<TrackingSystem> trackingSystems = new List<TrackingSystem>();

        [SerializeField] ToggleUserEvent parallaxToggleEvent;

        [SerializeField] public CalibratedTrackingData CurrentCalibratedTrackingData;
        [SerializeField] Transform calibrationTransform;
        [SerializeField] Dropdown calibrationDropdown;
        [SerializeField] Dropdown trackingSystemsDropdown;
        [SerializeField] Transform offAxisCam;
        [SerializeField] public Transform otherPlayerCalibration;
        public TrackingSystem CurrentTrackingSystem => currentTrackingSystem;

        public event MPDepthTrackingSource.TrackingDataUpdatedEvent TrackingDataUpdated;

        public List<TrackingSystem> TrackingSystems => trackingSystems;


        void OnEnable()
        {
            foreach (TrackingSystem trackingSystem in trackingSystems)
            {
                trackingSystem.TurnOff();
               // trackingSystemsDropdown.options.Add(new Dropdown.OptionData(trackingSystem.name));
            }
            instance = this;



            if (parallaxToggleEvent != null)
            {   
                parallaxToggleEvent.Toggled += ToggleParallax;
            }

            ChangeSystemTo(currentTrackingSystem);
            currentTrackingSystem.ScreenCalibrationProvider.Calibrate();

            // SelectCalibration(0);

        }

        void ToggleParallax(bool parallaxIsOn)
        {
            if (parallaxIsOn) offAxisCameraRig.EnableCameraTracking();
            else offAxisCameraRig.DisableCameraTracking();
        }



        void TrackingDataWasUpdated(MPDepthTrackingData data)
        {
            //TrackingDataUpdated?.Invoke(data);
            Vector3 calibratedPosition = CalibrateTrackingData.instance.CalibrateData(data, calibrationTransform)[0];
            ApplyCalibration(data);
            offAxisCameraRig.UpdateCameraLocation(calibratedPosition);
            //offAxisCameraRig.UpdateCameraLocation(data.CameraTrackingData.Position);
        }
        public void StartCalibration()
        {
            currentTrackingSystem.TrackingCalibrationProvider.Calibrate();
            currentTrackingSystem.ScreenCalibrationProvider.Calibrate();
            calibrationTransform.position = Vector3.zero;
            calibrationTransform.eulerAngles = Vector3.zero;
            offAxisCam.position = new Vector3(0, 0, offAxisCam.position.z);
            
            //UpdateCalibrationDropdown();
            
        }

        
        void ApplyCalibration(MPDepthTrackingData data)
        {
            

            CurrentCalibratedTrackingData.CameraTrackingData.Position = calibrationTransform.TransformPoint(data.CameraTrackingData.Position);
            CurrentCalibratedTrackingData.CameraTrackingData.Eulers = data.CameraTrackingData.Eulers;
            CurrentCalibratedTrackingData.BlendshapeTrackingData.Blendshapes = data.BlendshapeTrackingData.Blendshapes;
            //FindObjectOfType<ProjectionPlane>().UpdateOrientation();
        }

        public void SelectCalibration(Int32 index)
        {
            if (currentTrackingSystem.TrackingCalibrationProvider.AllCalibrations.Count > 0) currentTrackingSystem.TrackingCalibrationProvider.SelectCalibration(index);
            if (currentTrackingSystem.ScreenCalibrationProvider.AllCalibrations.Count > 0) currentTrackingSystem.ScreenCalibrationProvider.SelectCalibration(index);
            else currentTrackingSystem.ScreenCalibrationProvider.Calibrate();
        }
        void OnDisable()
        {
            currentTrackingSystem.TrackingDataUpdated -= TrackingDataWasUpdated;

            if (parallaxToggleEvent != null)
            {
                parallaxToggleEvent.Toggled -= ToggleParallax;
            }
        }


        public void SelectSystem(int selectedIndex)
        {
            if (selectedIndex > 0)
            {
                TrackingSystem newTrackingSystem;
                try
                {
                    newTrackingSystem = trackingSystems[selectedIndex - 1];
                }
                catch (IndexOutOfRangeException)
                {
                    if (TrackingSystems.Count == 0)
                    {
                        Debug.LogError("No Tracking Systems defined!");
                        throw;
                    }

                    Debug.LogWarning($"Saved Tracking System index out of range {selectedIndex}, reverting to default.");
                    newTrackingSystem = trackingSystems[0];

                }
                ChangeSystemTo(newTrackingSystem);
                SelectCalibration(0);
            }
        }

        public void ChangeSystemTo(TrackingSystem newTrackingSystem)
        {

            if (currentTrackingSystem != null)
            {
                currentTrackingSystem.TrackingDataUpdated -= TrackingDataWasUpdated;
                currentTrackingSystem.TurnOff();
            }

            currentTrackingSystem = newTrackingSystem;
            offAxisCameraRig.Screen = currentTrackingSystem.ScreenCalibrationProvider;

            currentTrackingSystem.TurnOn();
            currentTrackingSystem.TrackingDataUpdated += TrackingDataWasUpdated;
            //UpdateCalibrationDropdown();
            SelectCalibration(0);

        }

        public void UpdateCalibrationDropdown()
        {
            calibrationDropdown.ClearOptions();
            for(int i = 0; i < currentTrackingSystem.TrackingCalibrationProvider.AllCalibrations.Count; i++)
            {
                calibrationDropdown.options.Add(new Dropdown.OptionData(currentTrackingSystem.TrackingCalibrationProvider.AllCalibrations[i].Name));
            }
        }

        public void CycleTrackingSystem()
        {
            //currentTrackingSystem.TurnOff();
            for (int i = 0; i < trackingSystems.Count; i++)
            {
                if (trackingSystems[i] == currentTrackingSystem)
                {
                    currentTrackingSystemIndex = i;
                }
                //trackingSystems[i].TurnOff();
            }
            if (currentTrackingSystemIndex < trackingSystems.Count - 1)
            {
                ChangeSystemTo(trackingSystems[currentTrackingSystemIndex + 1]);

            }
            else
            {
                ChangeSystemTo(trackingSystems[0]);

            }
        }
        public Transform GetCalibrationTransform()
        {
            return calibrationTransform;
        }
    }

    
        
}
