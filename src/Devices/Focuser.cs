using System.Collections.Generic;
using System.Diagnostics;


namespace SGProTexter.SgProAPI
{
    internal class Focuser
    {
        private readonly SgGetDeviceStatus PtrSgGetDeviceStatus = new();

        public List<string> GetFocuserModelList(string WhoCalled)
        {
            SgEnumerateDevices PtrSgEnumerateDevices = new();
            SgEnumerateDevicesResponse PtrSgEnumerateDevicesResponse = new();
            Trace.Write(FileHostData.GetLoggingTime("Focuser.GetFocuserModelList::(Caller->" + WhoCalled + ") \n"));
            PtrSgEnumerateDevicesResponse = PtrSgEnumerateDevices.GetEnumerateDevices(SgApiTypes.DeviceType.Focuser, "Focuser.GetFocuserModelList()");
            List<string> RocuserList = new List<string>(PtrSgEnumerateDevicesResponse.Devices);
            return RocuserList;
        }


        public bool ConnectFocuser(string WhoCalled, string aDeviceName)
        {
            Trace.Write(FileHostData.GetLoggingTime("Focuser.ConnectFocuser()::(Caller->" + WhoCalled + ") \n"));
            bool results = IsFocuserConnected("Focuser.ConnectFocuser()");
            if (!results)
            {
                SgConnectDevice ptrSgConnectDevice = new SgConnectDevice();
                SgConnectDeviceResponse PtrSgConnectDeviceResponse = new SgConnectDeviceResponse();
                PtrSgConnectDeviceResponse = ptrSgConnectDevice.ConnectDevice(SgApiTypes.DeviceType.Focuser, aDeviceName, "Focuser.ConnectFocuser()");
                Trace.Write(FileHostData.GetLoggingTime("Focuser.ConnectFocuser()::(Caller->" + WhoCalled + ") \n"));
                results = IsFocuserConnected("ConnectFocuser()");
            }
            return results;
        }

        public bool DisconnectFocuser(string WhoCalled, string aDeviceName)
        {
            Trace.Write(FileHostData.GetLoggingTime("Focuser.DisconnectFocuser()::(Caller->" + WhoCalled + ") \n"));
            bool results = IsFocuserConnected("Focuser.DisconnectFocuser()");
            if (results)
            {
                SgDisconnectDevice ptrSgDisconnectDevice = new SgDisconnectDevice();
                SgDisconnectDeviceResponse PtrSgDisconnectDeviceResponse = new SgDisconnectDeviceResponse();
                PtrSgDisconnectDeviceResponse = ptrSgDisconnectDevice.DisconnectDevice(SgApiTypes.DeviceType.Focuser, "Focuser.DisconnectFocuser()");
                Trace.Write(FileHostData.GetLoggingTime("Focuser.DisconnectFocuser::(Caller->" + WhoCalled + ") \n"));
                results = IsFocuserConnected("DisconnectFocuser()");
            }
            return results;
        }

        public bool IsFocuserConnected(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("Focuser.IsFocuserConnected()::(Caller->" + WhoCalled + ") \n"));
            SgGetDeviceStatusResponse PtrDeviceStatusResponse = PtrSgGetDeviceStatus.GetFocuserStatus("Focuser.IsFocuserConnected()");
            return PtrDeviceStatusResponse.IsConnected;
        }

        public bool SetFocuserPosition(string aPosition, string WhoCalled)
        {
            SgSetFocuserPosition PtrSgSetFocuserPosition = new SgSetFocuserPosition();
            Trace.Write(FileHostData.GetLoggingTime("Focuser.SetFocuserPosition()::(Caller->" + WhoCalled + ") \n"));
            bool results = false;
            SgSetFocuserPositionResponse PtrSgSetFocuserPositionResponse = new();
            SgSetFocuserPositionPostBody PtrPostBody = new SgSetFocuserPositionPostBody();
            PtrPostBody.Position = aPosition;
            PtrSgSetFocuserPositionResponse = PtrSgSetFocuserPosition.SetFocuserPosition(PtrPostBody, "Focuser.SetFocuserPosition()");
            return results;
        }

        public int GetFocuserPosition(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("Focuser.GetFocuserPosition()::(Caller->" + WhoCalled + ") \n"));
            SgGetFocuserPositionResponse PtrGetFocuserPositionResponse = new();
            PtrGetFocuserPositionResponse = SgGetFocuserPosition.GetFocuserPosition("Focuser.GetFocuserPosition()");
            return PtrGetFocuserPositionResponse.Position;
        }
    }
}
