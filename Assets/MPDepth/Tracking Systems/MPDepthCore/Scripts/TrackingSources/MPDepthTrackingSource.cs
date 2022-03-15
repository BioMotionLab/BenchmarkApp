using MPDepthCore.Calibration.Camera;
using UnityEngine;

namespace MPDepthCore.TrackingSources {
    public abstract class MPDepthTrackingSource : MonoBehaviour {
        
        public MPDepthTrackingData TrackingData = new RawTrackingData();
        
        public delegate void TrackingDataUpdatedEvent(MPDepthTrackingData data);

        public abstract event TrackingDataUpdatedEvent TrackingDataUpdated;


        public abstract void TurnOff();

        public abstract void TurnOn();
    }
}