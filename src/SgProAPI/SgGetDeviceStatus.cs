using Newtonsoft.Json;
using RestSharp;
using SGProTexter.SgProAPI;
using System;
using System.Diagnostics;

namespace SGProTexter
{
    /*

    SgGetDeviceStatus

    http://localhost:59590/json/reply/SgGetDeviceStatus?format=json
    results: {"State":"DISCONNECTED","Success":true,"Message":"There is no camera connected!"}

     
    Query a device to get its status. 
    Possible states are IDLE, INTEGRATING, DOWNLOADING, SOLVING, READY, BUSY, ABORTED, 
    MOVING, DISCONNECTED, PARKED, UNKNOWN.


    The following routes are available for this service: 
        All Verbs   /devicestatus/{Device}   
        POST        /devicestatus/ 

        The device type to query with values "Camera", "FilterWheel", "Focuser", "Telescope" and "PlateSolver".
        Will query the selected device for status.

        States are "IDLE", "CAPTURING", "SOLVING", "BUSY", "MOVING", "DISCONNECTED", "PARKED".

    To override the Content-type in your clients HTTP Accept Header, append ?format=json
    To embed the response in a jsonp callback, append ?callback=myCallback

    URL should look like this http://localhost:59590/devicestatus/Camera?format=json
    will return: {"State":"DISCONNECTED","Success":true,"Message":"There is no camera connected!"}

    */


    internal class SgGetDeviceStatus
    {
        public SgGetDeviceStatusResponse PtrResponse = new SgGetDeviceStatusResponse();
        public SgGetDeviceStatusPlaceholder PtrPlaceHolder = new SgGetDeviceStatusPlaceholder();

        public SgGetDeviceStatusResponse GetCameraStatus(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetDeviceStatus.GetCameraStatus()::(Caller->" + WhoCalled + ")\n"));
            DoDeviceStatus(SgApiTypes.DeviceType.Camera, "GetCameraStatus()", WhoCalled);
            return PtrResponse;
        }

        public SgGetDeviceStatusResponse GetFilterWheelStatus(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetDeviceStatus.GetFilterWheelStatus()::(Caller->" + WhoCalled + ")\n"));
            DoDeviceStatus(SgApiTypes.DeviceType.FilterWheel, "GetFilterWheelStatus()", WhoCalled);
            return PtrResponse;
        }

        public SgGetDeviceStatusResponse GetFocuserStatus(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetDeviceStatus.GetFocuserStatus()::(Caller->" + WhoCalled + ")\n"));
            DoDeviceStatus(SgApiTypes.DeviceType.Focuser, "GetFocuserStatus()", WhoCalled);
            return PtrResponse;
        }

        public SgGetDeviceStatusResponse GetTelescopeStatus(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetDeviceStatus.GetTelescopeStatus()::(Caller->" + WhoCalled + ")\n"));
            DoDeviceStatus(SgApiTypes.DeviceType.Telescope, "GetTelescopeStatus()", WhoCalled);
            return PtrResponse;
        }

        public SgGetDeviceStatusResponse GetPlateSolverStatus(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetDeviceStatus.GetPlateSolverStatus()::(Caller->" + WhoCalled + ")\n"));
            DoDeviceStatus(SgApiTypes.DeviceType.PlateSolver, "GetPlateSolverStatus()", WhoCalled);
            return PtrResponse;
        }


        void DoDeviceStatus(string aDeviceName, string DeviceCaller, string WhoCalled)
        {
            string aWhoCalled = "SgGetDeviceStatus." + DeviceCaller + "DoDeviceStatus()::(Caller->" + WhoCalled + ")";
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath + "/" + aDeviceName;
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetDeviceStatusResponse>(response.Content);
                if (PtrResponse.State.ToUpper() == SgApiTypes.StateType.DISCONNECTED)
                {
                    PtrResponse.IsConnected = false;
                }
                else
                {
                    PtrResponse.IsConnected = true;
                }
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Connected: \t" + PtrResponse.IsConnected + "\n");
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetDeviceStatus.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Connected: \t" + PtrResponse.IsConnected + "\n");
        }
    }

    /*
    Responses POST
    All Verbs /devicestatus/{Device}   

    POST /json/reply/SgGetDeviceStatus HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Device":"Camera"}

    */
    internal class SgGetDeviceStatusPlaceholder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/devicestatus";
    }

    /* 
    Request GET 
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"State":"IDLE","Success":false,"Message":"String"}
    */

    internal class SgGetDeviceStatusResponse : SgBaseResponse
    {
        public bool IsConnected { get; set; }
    }
}
