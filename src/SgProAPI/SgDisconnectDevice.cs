using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgDisconnectDevice


    http://localhost:59590/json/reply/SgConnectDevice?format=json
    results:    {"Success":true,"Message":"Connected to "}

    Disconnect from a device.
    The following routes are available for this service: 
        All Verbs /disconnectdevice/{Device}   

    Parameters: 
    Name    Parameter Data Type     Required    Description 
    Device  path      string        Yes         The device type to disconnect. Values are "Camera", "FilterWheel", "Focuser", "Telescope" and "Rotator". 


    
 */

namespace SGProTexter.SgProAPI
{
    internal class SgDisconnectDevice
    {
        SgDisconnectDeviceResponse PtrResponse = new SgDisconnectDeviceResponse();
        SgDisconnectDevicePlaceHolder PtrPlaceHolder = new SgDisconnectDevicePlaceHolder();
        private string Device { get; set; }

        public SgDisconnectDeviceResponse DisconnectDevice(string aDevice, string WhoCalled)
        {
            Device = aDevice;
            Trace.Write(FileHostData.GetLoggingTime("SgDisconnectDevice.DisconnectDevice()::(Caller->" + WhoCalled + " Device: " + Device + ")\n"));
            DoDisconnectDevice(WhoCalled);
            return PtrResponse;
        }


        private void DoDisconnectDevice(string WhoCalled)
        {
            string aWhoCalled = "SgDisconnectDevice.DisconnectDevice().DoDisconnectDevice()::(Caller->" + WhoCalled + ")";
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath + "/" + Device;
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgDisconnectDeviceResponse>(response.Content);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgDisconnectDevice.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }
    }

    /*
     
    Responses POST 
    All Verbs /disconnectdevice/{Device} 
    
    POST /json/reply/SgDisconnectDevice HTTP/1.1 
    Host: localhost
    Content-Type: application/json
    Content-Length: length

    {"Device":"Camera"}

    Values are "Camera", "FilterWheel", "Focuser", "Telescope" and "Rotator". 

    Example: Enter this url into browser
    http://localhost:59590/disconnectdevice/FilterWheel?format=json
    Results: {"Success":true,"Message":"Device disconnected"}

    */


    internal class SgDisconnectDevicePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/disconnectdevice";
    }

    /*
    
    Request GET
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String"}

    */

    internal class SgDisconnectDeviceResponse : SgBaseResponse
    {

    }
}
