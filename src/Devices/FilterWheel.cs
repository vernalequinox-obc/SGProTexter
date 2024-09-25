using System.Collections.Generic;
using System.Diagnostics;


namespace SGProTexter.SgProAPI
{
    internal class FilterWheel
    {
        private readonly SgGetDeviceStatus PtrSgGetDeviceStatus = new();

        public List<string> GetFilterWheelModelList(string WhoCalled)
        {
            SgEnumerateDevices PtrSgEnumerateDevices = new();
            SgEnumerateDevicesResponse PtrSgEnumerateDevicesResponse = new();
            Trace.Write(FileHostData.GetLoggingTime("FilterWheel.GetFilterWheelModelList::(Caller->" + WhoCalled + ") \n"));
            PtrSgEnumerateDevicesResponse = PtrSgEnumerateDevices.GetEnumerateDevices(SgApiTypes.DeviceType.FilterWheel, "FilterWheel.GetFilterWheelModelList()");
            List<string> RocuserList = new List<string>(PtrSgEnumerateDevicesResponse.Devices);
            return RocuserList;
        }


        public bool ConnectFilterWheel(string WhoCalled, string aDeviceName)
        {
            Trace.Write(FileHostData.GetLoggingTime("FilterWheel.ConnectFilterWheel()::(Caller->" + WhoCalled + ") \n"));
            bool results = IsFilterWheelConnected("FilterWheel.ConnectFilterWheel()");
            if (!results)
            {
                SgConnectDevice ptrSgConnectDevice = new SgConnectDevice();
                SgConnectDeviceResponse PtrSgConnectDeviceResponse = new SgConnectDeviceResponse();
                PtrSgConnectDeviceResponse = ptrSgConnectDevice.ConnectDevice(SgApiTypes.DeviceType.FilterWheel, aDeviceName, "FilterWheel.ConnectFilterWheel()");
                Trace.Write(FileHostData.GetLoggingTime("FilterWheel.ConnectFilterWheel()::(Caller->" + WhoCalled + ") \n"));
                results = IsFilterWheelConnected("ConnectFilterWheel()");
            }
            return results;
        }

        public bool DisconnectFilterWheel(string WhoCalled, string aDeviceName)
        {
            Trace.Write(FileHostData.GetLoggingTime("FilterWheel.DisconnectFilterWheel()::(Caller->" + WhoCalled + ") \n"));
            bool results = IsFilterWheelConnected("FilterWheel.DisconnectFilterWheel()");
            if (results)
            {
                SgDisconnectDevice ptrSgDisconnectDevice = new SgDisconnectDevice();
                SgDisconnectDeviceResponse PtrSgDisconnectDeviceResponse = new SgDisconnectDeviceResponse();
                PtrSgDisconnectDeviceResponse = ptrSgDisconnectDevice.DisconnectDevice(SgApiTypes.DeviceType.FilterWheel, "FilterWheel.DisconnectFilterWheel()");
                Trace.Write(FileHostData.GetLoggingTime("FilterWheel.DisconnectFilterWheel::(Caller->" + WhoCalled + ") \n"));
                results = IsFilterWheelConnected("DisconnectFilterWheel()");
            }
            return results;
        }

        public bool IsFilterWheelConnected(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("FilterWheel.IsFilterWheelConnected()::(Caller->" + WhoCalled + ") \n"));
            SgGetDeviceStatusResponse PtrDeviceStatusResponse = PtrSgGetDeviceStatus.GetFilterWheelStatus("FilterWheel.IsFilterWheelConnected()");
            return PtrDeviceStatusResponse.IsConnected;
        }

        public bool SetFilterWheelPosition(string aPosition, string WhoCalled)
        {
            SgSetFilterPosition PtrSgSetFilterPosition = new SgSetFilterPosition();
            Trace.Write(FileHostData.GetLoggingTime("FilterWheel.SetFilterWheelPosition()::(Caller->" + WhoCalled + ")  \n"));
            bool results = false;
            SgSetFilterPositionResponse PtrSgSetFilterPositionResponse = new();
            SgSetFilterPositionPostBody PtrPostBody = new SgSetFilterPositionPostBody();
            PtrPostBody.Position = aPosition;
            PtrSgSetFilterPositionResponse = PtrSgSetFilterPosition.SetFilterPosition(PtrPostBody, "FilterWheel.SetFilterWheelPosition()");
            return results;
        }

        public int GetFilterWheelPosition(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("FilterWheel.GetFilterWheelPosition()::(Caller->" + WhoCalled + ") \n"));
            SgGetFilterPositionResponse PtrSgGetFilterPositionResponse = new();
            PtrSgGetFilterPositionResponse = SgGetFilterPosition.GetFilterWheelPosition("FilterWheel.GetFilterWheelPosition()");
            return PtrSgGetFilterPositionResponse.Position;
        }
    }
}
