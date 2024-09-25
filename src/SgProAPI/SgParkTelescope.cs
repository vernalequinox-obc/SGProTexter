using Newtonsoft.Json;
using RestSharp;
using SGProTexter.SgProAPI;
using System;
using System.Diagnostics;

/*
    
    SgParkTelescope


    http://localhost:59590/json/reply/SgParkTelescope?format=json
    results:  {"Success":false,"Message":"Telescope not connected."}

    Park or unpark the telescope.
    The following routes are available for this service: 
            All Verbs /ParkTelescope

    Parameters: 
    Name    Parameter   Data Type   Required    Description 
    Park    path        bool        Yes         A value of True will park the scope and False will unpark. 

    To override the Content-type in your clients HTTP Accept Header, append ?format=jsv


 */

namespace SGProTexter
{
    internal class SgParkTelescope
    {
        SgParkTelescopeResponse PtrResponse = new SgParkTelescopeResponse();
        SgParkTelescopePlaceHolder PtrPlaceHolder = new SgParkTelescopePlaceHolder();
        SgParkTelescopePostBody PtrPostBody = new SgParkTelescopePostBody();

        public SgParkTelescopeResponse ParkTelescope(SgParkTelescopePostBody aPostBody, string WhoCalled)
        {
            PtrPostBody = aPostBody;
            Trace.Write(FileHostData.GetLoggingTime("SgParkTelescope.ParkTelescope()::(Caller->" + WhoCalled + ")\n"));
            DoParkTelescope(WhoCalled);
            return PtrResponse;
        }

        private void DoParkTelescope(string WhoCalled)
        {
            object AddBody = PtrPostBody;
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            string aWhoCalled = "SgParkTelescope.ParkTelescope().DoParkTelescope()::(Caller->" + WhoCalled + ") + Park: " + PtrPostBody.Park;
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.PostPlaceHolder(PtrPlaceHolder.RequestResourceString, PtrPostBody));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgParkTelescopeResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgParkTelescope.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }

    }

    /*
    Responses POST

    All Verbs /ParkTelescope 

    POST /json/reply/SgParkTelescope HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length
    
    {"Park":false}
       
     
    */

    internal class SgParkTelescopePostBody
    {
        public string Park { get; set; }
    }

    internal class SgParkTelescopePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/ParkTelescope";
    }

    /*
    Request GET

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length
    
    {"Success":false,"Message":"String"}

    */

    internal class SgParkTelescopeResponse : SgBaseResponse
    {
    }
}
