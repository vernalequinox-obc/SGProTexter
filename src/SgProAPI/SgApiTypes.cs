/*
  * URL should look like this http://localhost:59590/devicestatus/Camera?format=json
  * will return: {"State":"DISCONNECTED","Success":true,"Message":"There is no camera connected!"}
 */

namespace SGProTexter
{
    internal class SgApiTypes
    {
        public readonly struct ImageType
        {
            public const string Light = "Light";
            public const string Dark = "Dark";
            public const string Bias = "Bias";
            public const string Flat = "Flat";
        }

        public readonly struct CameraSpeed
        {
            public const string Normal = "Normal";
            public const string HiSpeed = "HiSpeed";
        }

        public readonly struct DeviceType
        {
            public const string Camera = "Camera";
            public const string FilterWheel = "FilterWheel";
            public const string Focuser = "Focuser";
            public const string Telescope = "Telescope";
            public const string Rotator = "Rotator";
            public const string PlateSolver = "PlateSolver";
        }

        public readonly struct StateType
        {
            public const string IDLE = "IDLE";
            public const string INTEGRATING = "INTEGRATING";
            public const string DOWNLOADING = "DOWNLOADING";
            public const string SOLVING = "SOLVING";
            public const string READY = "READY";
            public const string BUSY = "BUSY";
            public const string ABORTED = "ABORTED";
            public const string MOVING = "MOVING";
            public const string DISCONNECTED = "DISCONNECTED";
            public const string PARKED = "PARKED";
            public const string UNKNOWN = "UNKNOWN";

            public static bool IsMatch(string aStateType)
            {
                string UpperStateType;
                if (aStateType == null) { return false; }
                else { UpperStateType = aStateType.ToString(); }
                if (UpperStateType == IDLE) { return true; }
                else if (UpperStateType == INTEGRATING) { return true; }
                else if (UpperStateType == DOWNLOADING) { return true; }
                else if (UpperStateType == SOLVING) { return true; }
                else if (UpperStateType == READY) { return true; }
                else if (UpperStateType == BUSY) { return true; }
                else if (UpperStateType == ABORTED) { return true; }
                else if (UpperStateType == MOVING) { return true; }
                else if (UpperStateType == DISCONNECTED) { return true; }
                else if (UpperStateType == PARKED) { return true; }
                else if (UpperStateType == UNKNOWN) { return true; }
                return false;
            }
        }

        public readonly struct ObservatoryOpenState
        {
            public const string Unknown = "Unknown";
            public const string Open = "Open";
            public const string Closed = "Closed";
            public const string Opening = "Opening";
            public const string Closing = "Closing";
            public const string Error = "Error";
        }
    }
}
