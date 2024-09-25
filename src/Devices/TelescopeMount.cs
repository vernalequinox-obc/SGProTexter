using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGProTexter.SgProAPI
{
    internal class TelescopeMount
    {
        private readonly SgGetDeviceStatus PtrSgGetDeviceStatus = new();

        public List<string> GetTelescopeMountModelList(string WhoCalled)
        {
            List<string> MountList;
            SgEnumerateDevices PtrSgEnumerateDevices = new();
            SgEnumerateDevicesResponse PtrSgEnumerateDevicesResponse = new();
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.GetTelescopeMountModelList::(Caller->" + WhoCalled + ") \n"));
            PtrSgEnumerateDevicesResponse = PtrSgEnumerateDevices.GetEnumerateDevices(SgApiTypes.DeviceType.Telescope, "TelescopeMount.GetTelescopeModelMountList()");
            if (PtrSgEnumerateDevicesResponse.Success)
            {
                MountList = new List<string>(PtrSgEnumerateDevicesResponse.Devices);
            }
            else
            {
                MountList = new List<string> { PtrSgEnumerateDevicesResponse.Message };
            }
            return MountList;
        }

        public bool ConnectTelescopeMount(string WhoCalled, string aDeviceName)
        {
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.ConnectTelescopeMount()::(Caller->" + WhoCalled + ") \n"));
            bool results = IsTelescopeMountConnected("TelescopeMount.ConnectTelescopeMount()");
            if (!results)
            {
                SgConnectDevice ptrSgConnectDevice = new SgConnectDevice();
                SgConnectDeviceResponse PtrSgConnectDeviceResponse = new SgConnectDeviceResponse();
                PtrSgConnectDeviceResponse = ptrSgConnectDevice.ConnectDevice(SgApiTypes.DeviceType.Telescope, aDeviceName, "TelescopeMount.ConnectTelescopeMount()");
                Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.ConnectTelescopeMount()::(Caller->" + WhoCalled + ") \n"));
                results = IsTelescopeMountConnected("ConnectTelescopeMount()");
            }
            return results;
        }

        public bool DisconnectTelescopeMount(string WhoCalled, string aDeviceName)
        {
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.DisconnectTelescopeMount()::(Caller->" + WhoCalled + ") \n"));
            bool results = IsTelescopeMountConnected("TelescopeMount.DisconnectTelescopeMount()");
            if (results)
            {
                SgDisconnectDevice ptrSgDisconnectDevice = new SgDisconnectDevice();
                SgDisconnectDeviceResponse PtrSgDisconnectDeviceResponse = new SgDisconnectDeviceResponse();
                PtrSgDisconnectDeviceResponse = ptrSgDisconnectDevice.DisconnectDevice(SgApiTypes.DeviceType.Telescope, "TelescopeMount.DisconnectTelescopeMount()");
                Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.DisconnectTelescopeMount::(Caller->" + WhoCalled + ") \n"));
                results = IsTelescopeMountConnected("DisconnectTelescopeMount()");
            }
            return results;
        }

        public bool IsTelescopeMountConnected(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.IsTelescopeMountConnected()::(Caller->" + WhoCalled + ") \n"));
            SgGetDeviceStatusResponse PtrDeviceStatusResponse = PtrSgGetDeviceStatus.GetTelescopeStatus("TelescopeMount.IsTelescopeMountConnected()");
            return PtrDeviceStatusResponse.IsConnected;
        }

        public void GetTelescopeMountPosition(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.GetTelescopeMountPosition()::(Caller->" + WhoCalled + ") \n"));
            SgGetTelescopePositionResponse PtrSgGetTelescopePositionResponse = new();
            SgGetTelescopePosition PtrSgGetTelescopePosition = new SgGetTelescopePosition();
            PtrSgGetTelescopePositionResponse = PtrSgGetTelescopePosition.GetTelescopeMountPosition("TelescopeMount.GetTelescopeMountPosition()");
            Ra = PtrSgGetTelescopePositionResponse.Ra;
            Dec = PtrSgGetTelescopePositionResponse.Dec;
        }

        public void ParkTelescopeMount(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.ParkTelescopeMount()::(Caller->" + WhoCalled + ") \n"));
            SgParkTelescope PtrSgParkTelescope = new SgParkTelescope();
            SgParkTelescopePostBody PtrPostBody = new SgParkTelescopePostBody();
            PtrPostBody.Park = "true";
            PtrSgParkTelescope.ParkTelescope(PtrPostBody, "TelescopeMount.ParkTelescopeMount()");
        }

        public void UnParkTelescopeMount(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.UnParkTelescopeMount()::(Caller->" + WhoCalled + ") \n"));
            SgParkTelescope PtrSgParkTelescope = new SgParkTelescope();
            SgParkTelescopePostBody PtrPostBody = new SgParkTelescopePostBody();
            PtrPostBody.Park = "false";
            PtrSgParkTelescope.ParkTelescope(PtrPostBody, "TelescopeMount.UnParkTelescopeMount()");
        }

        public void SlewTelescopeMount(string WhoCalled, string aRa, string aDec)
        {
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.SlewTelescopeMount()::(Caller->" + WhoCalled + ") \n"));
            SgSlewTelescope PtrSgSlewTelescope = new SgSlewTelescope();
            SgSlewTelescopePostBody PtrSgSlewTelescopePostBody = new SgSlewTelescopePostBody();
            PtrSgSlewTelescopePostBody.Ra = aRa;
            PtrSgSlewTelescopePostBody.Dec = aDec;
            PtrSgSlewTelescope.SlewTelescopePosition(PtrSgSlewTelescopePostBody, "TelescopeMount.SlewTelescopeMount()");
        }

        public float Ra { get; set; }
        public float Dec { get; set; }


        public string RaInTime(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.RaInTime()::(Caller->" + WhoCalled + "\n"));
            GetTelescopeMountPosition("TelescopeMount.RaInTime()");
            var RaTime = TimeSpan.FromHours(Ra);
            string RaInTime = $"{RaTime.Hours:00}h:{RaTime.Minutes:00}m:{RaTime.Seconds:00}.{RaTime.Milliseconds / 10.0:00}s";
            if (RaInTime.Contains('-'))
            {
                string negTime = RaInTime.Replace("-", "");
                RaInTime = "-" + negTime;
            }
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.RaInTime()::(Caller->" + WhoCalled + ")  RaTime: " + RaTime + "\n"));
            return RaInTime;
        }

        public string DecInTime(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.DecInTime()::(Caller->" + WhoCalled + "\n"));
            GetTelescopeMountPosition("TelescopeMount.RaInTime()");
            var DecTime = TimeSpan.FromHours(Dec);
            string DecInTime = $"{DecTime.Hours:00}°{DecTime.Minutes:00}\'{DecTime.Seconds:00}.{DecTime.Milliseconds / 10.0:00}\"";
            if (DecInTime.Contains('-'))
            {
                string negTime = DecInTime.Replace("-", "");
                DecInTime = "-" + negTime;
            }
            Trace.Write(FileHostData.GetLoggingTime("TelescopeMount.DecInTime()::(Caller->" + WhoCalled + ")  DecTime: " + DecTime + "\n"));
            return DecInTime;
        }
    }
}
