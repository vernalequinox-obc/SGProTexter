using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

    SgConnectDevice
    
    http://localhost:59590/json/reply/SgConnectDevice
    Results:{"Success":true,"Message":"Connected to "}

    Connect to a device in SGPro. Use "/enumdevices" to get a list of avaialable devices.
    The following routes are available for this service: 
        All Verbs /connectdevice/{Device}/{DeviceName}   
    
    Parameters: 
    Name        Parameter   Data Type   Required    Description 
    Device      path        string      Yes         The device type to connect. Values are "Camera", "FilterWheel", "Focuser", "Telescope" and "Rotator". 
    DeviceName  path        string      Yes         The device name. To get a list of device names, call "/enumdevices" or "SgEnumerateDevices". 
    
    To override the Content-type in your clients HTTP Accept Header, append ?format=json
    
    To embed the response in a jsonp callback, append ?callback=myCallback

    Connects a device. Use the EnumerateDevices to get a list of possible device models for a device type.
     
    Example: {device} is camera and the {DeviceName} (model of the cameras) is "Camera V2 simulator"
     
    http://localhost:59590/connectdevice/Camera/Camera V2 simulator?format=json
     
    Results:
    {"Success":true,"Message":"Connected to Camera V2 simulator"}

 */



namespace SGProTexter.SgProAPI
{
    internal class SgConnectDevice
    {
        SgConnectDeviceResponse PtrResponse = new SgConnectDeviceResponse();
        SgConnectDevicePlaceHolder PtrPlaceHolder = new SgConnectDevicePlaceHolder();
        private string Device { get; set; }
        private string DeviceName { get; set; }

        public SgConnectDeviceResponse ConnectDevice(string aDevice, string aDeviceName, string WhoCalled)
        {
            Device = aDevice;
            DeviceName = aDeviceName;
            Trace.Write(FileHostData.GetLoggingTime("SgConnectDevice.ConnectDevice()::(Caller->" + WhoCalled + " Device: " + aDevice + " DeviceName: " + aDeviceName + ")\n"));
            DoConnectionDevice(aDevice, aDeviceName, "GetFocuser()");
            return PtrResponse;
        }

        private void DoConnectionDevice(string aDevice, string aDeviceName, string WhoCalled)
        {
            string aWhoCalled = "SgConnectDevice.ConnectDevice().DoConnectionDevice()::(Caller->" + WhoCalled + ")";
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath + "/" + Device + "/" + DeviceName;
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgConnectDeviceResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
            else
            {
                PtrResponse.Success = false;
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgConnectDevice.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }
    }


    /*
    Responses POST
    All Verbs /connectdevice/{Device}/{DeviceName}   

    POST /json/reply/SgConnectDevice HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Device":"Camera","DeviceName":"String"}

    Example: {device} is camera and the {DeviceName} (model of the cameras) is "Camera V2 simulator"

    http://localhost:59590/connectdevice/Camera/Camera V2 simulator?format=json
    output will be:  {"Success":true,"Message":"Connected to Camera V2 simulator"}

    */

    internal class SgConnectDevicePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/connectdevice";
    }

    /*
    Request GET
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String"}

    */
    internal class SgConnectDeviceResponse : SgBaseResponse
    {

    }
}
