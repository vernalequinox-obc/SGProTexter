using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
 * 
 *   SgEnumerateDevices 
 *   
    http://localhost:59590/json/reply/SgEnumerateDevices?format=json
    results:   {"Devices":["Camera V2 simulator","Canon EOS","FLI USB Camera","Nikon","QSI CCD Camera","SBIG Camera","Simulator","ZWO Camera"],"Success":true}

 *   
 *   get a list of model or manufactor of that type of device.
 *  
 *  Example calling the following:
 *  http://localhost:59590/enumdevices/camera?format=json
 *  results are:
 *  {"Devices":["Camera V2 simulator","Canon EOS","FLI USB Camera","Nikon","QSI CCD Camera","SBIG Camera","Simulator","ZWO Camera"],"Success":true}
 *
 *
 *
 *
 * Get a list of available devices by type.
 * The following routes are available for this service:
 * All Verbs	/enumdevices/{Device}		
 * Parameters:
 * Name	Parameter	Data Type	Required	Description
 * Device	path	string	Yes	The device type to enumerate. Values are "Camera", "FilterWheel", "Focuser", "Telescope" and "Rotator".
 * To override the Content-type in your clients HTTP Accept Header, append ?format=json
 * 
 * To embed the response in a jsonp callback, append ?callback=myCallback

 * HTTP + JSON
 * The following are sample HTTP requests and responses. The placeholders shown need to be replaced with actual values.

 * POST /json/reply/SgEnumerateDevices HTTP/1.1 
 * Host: localhost 
 * Content-Type: application/json
 * Content-Length: length

 * {"Device":"Camera"}
 * HTTP/1.1 200 OK
 * Content-Type: application/json
 * Content-Length: length
 * 
 * {"Devices":["String"],"Success":false,"Message":"String"}
 * 
 * 
 */


namespace SGProTexter.SgProAPI
{
    internal class SgEnumerateDevices
    {
        SgEnumerateDevicesResponse PtrResponse = new SgEnumerateDevicesResponse();
        SgEnumerateDevicesPlaceHolder PtrPlaceHolder = new SgEnumerateDevicesPlaceHolder();
        private string EnumerateDevices = string.Empty;

        public SgEnumerateDevicesResponse GetEnumerateDevices(string aDevice, string WhoCalled)
        {
            EnumerateDevices = aDevice;
            Trace.Write(FileHostData.GetLoggingTime("SgEnumerateDevices.GetEnumerateDevices()::(Caller->" + WhoCalled + ")\n"));
            DoGetEnumerateDevices(WhoCalled);
            return PtrResponse;
        }

        void DoGetEnumerateDevices(string WhoCalled)
        {
            string aWhoCalled = "SgEnumerateDevices.GetEnumerateDevices().DoGetEnumerateDevices()::(Caller->" + WhoCalled + ")";
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath + "/" + EnumerateDevices;
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgEnumerateDevicesResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                string aMessage = "";
                foreach (var device in PtrResponse.Devices)
                {
                    aMessage += "\t\t\t Devices:   \t" + device + "\n";
                }
                Trace.Write(aMessage);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgEnumerateDevices.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            string aMessage = "";
            foreach (var device in PtrResponse.Devices)
            {
                aMessage += "\t\t\t Devices:   \t" + device + "\n";
            }
            Console.Write(aMessage);
        }
    }


    /*
    Responses POST
    All Verbs	/enumdevices/{Device}

    The device type to enumerate. Values are "Camera", "FilterWheel", "Focuser", "Telescope" and "Rotator".
    POST /json/reply/SgEnumerateDevices HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Device":"Camera"}
    */

    internal class SgEnumerateDevicesPostBody
    {
        public string Device { get; set; }
    }

    internal class SgEnumerateDevicesPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/enumdevices";
    }

    /*
    Request GET 
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Devices":["String"],"Success":false,"Message":"String"}
    */

    internal class SgEnumerateDevicesResponse : SgBaseResponse
    {
        public string[] Devices { get; set; }
    }

}

