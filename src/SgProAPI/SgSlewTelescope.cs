using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
    SgSlewTelescope



    http://localhost:59590/json/reply/SgSlewTelescope?format=json
    results:{"Success":true,"Message":"Setting telescope to location: RA=0; DEC=0..."}

    Slew the telescope to the given RA and DEC. This call is asynchronous. To check status, 
    you can either call "/devicestatus/Telescope" or periodically check position with "/telescopepos". 
    If this call is successful, it means that the telescope successfully received the message, not that the change is position was successful.
    
    The following routes are available for this service: POST /slewtelescopepos   
            All Verbs /slewtelescopepos/{Position}   

    Parameters: 
    Name    Parameter   Data Type   Required    Description 
    Ra      path        float       Yes         The Right Ascension in decimal hours 
    Dec     path        float       Yes         The Declination in decimal degrees 

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback


*/

namespace SGProTexter.SgProAPI
{
    internal class SgSlewTelescope
    {

        SgSlewTelescopeResponse PtrResponse = new SgSlewTelescopeResponse();
        SgSlewTelescopePlaceHolder PtrPlaceHolder = new SgSlewTelescopePlaceHolder();
        SgSlewTelescopePostBody PtrPostBody = new SgSlewTelescopePostBody();

        public SgSlewTelescopeResponse SlewTelescopePosition(SgSlewTelescopePostBody aPostBody, string WhoCalled)
        {
            PtrPostBody = aPostBody;
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            Trace.Write(FileHostData.GetLoggingTime("SgSlewTelescopePosition.SlewTelescopePosition()::(Caller->" + WhoCalled + ")\n"));
            Trace.Write("\t\t\t Ra:    \t" + PtrPostBody.Ra + "\n");
            Trace.Write("\t\t\t Dec:   \t" + PtrPostBody.Dec + "\n");

            DoSlewTelescopePosition(WhoCalled);
            return PtrResponse;
        }

        private void DoSlewTelescopePosition(string WhoCalled)
        {
            object AddBody = "{\"Ra\":0,\"Dec\":0}";
            string aWhoCalled = "SgSlewTelescopePosition.SlewTelescopePosition().DoSlewTelescopePosition()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.PostPlaceHolder(PtrPlaceHolder.RequestResourceString, PtrPostBody));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgSlewTelescopeResponse>(response.Content);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgSlewTelescopePosition.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Ra:    \t" + PtrResponse.Ra + "\n");
            Console.Write("\t\t\t Dec:   \t" + PtrResponse.Dec + "\n");
        }
    }

    /*
    Responses POST

    All Verbs /slewtelescopepos/{Position}   

    POST /json/reply/SgSlewTelescope HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Ra":0,"Dec":0}

 
    */

    internal class SgSlewTelescopePostBody
    {
        public string Ra { get; set; }
        public string Dec { get; set; }
    }

    internal class SgSlewTelescopePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/slewtelescopepos";
    }

    /*
    Request GET

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String"}


    */

    internal class SgSlewTelescopeResponse : SgBaseResponse
    {
        public float Ra { get; set; }
        public float Dec { get; set; }
    }
}