using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGProTexter.SgProAPI
{
    internal class Camera
    {
        private readonly SgGetDeviceStatus PtrSgGetDeviceStatus = new();
        public bool IsCameraConnected(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("Camera.IsCameraConnected()::(Caller->" + WhoCalled + ") \n"));
            SgGetDeviceStatusResponse PtrDeviceStatusResponse = PtrSgGetDeviceStatus.GetCameraStatus("Camera.IsCameraConnected()");
            return PtrDeviceStatusResponse.IsConnected;
        }
    }
}
