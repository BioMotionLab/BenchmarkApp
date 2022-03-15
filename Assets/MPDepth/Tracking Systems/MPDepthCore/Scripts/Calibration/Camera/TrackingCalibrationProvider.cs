using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace MPDepthCore.Calibration.Camera {
    public abstract class TrackingCalibrationProvider : MonoBehaviour {
        [SerializeField] public Transform calibrationTransform;
        public abstract TrackerOffsetCalibration GetTrackerOffsetCalibration {get; }
        public abstract SavedTrackerCalibration CurrentTrackerCalibration { get; }
        
        public abstract List<SavedTrackerCalibration> AllCalibrations { get; }
        

        public abstract void Calibrate();

        void OnEnable() {
         
        }



        
        void OnDisable() {
           
        }
        
        protected string FilePath => Path.Combine(BaseFolder, Filename);
        protected abstract string Filename { get; }

        protected string BaseFolder => Path.Combine(Application.persistentDataPath, "Save");


        public abstract void SelectCalibration(int selectedIndex);
    }

    public interface SavedTrackerCalibration : ICalibration {
        
    }
}