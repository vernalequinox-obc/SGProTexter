using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgGetSolvedImageData

    http://localhost:59590/json/reply/SgGetSolvedImageData?format=json
    results:  {"Success":false,"Message":"Solving","Ra":0,"Dec":0,"Scale":0,"Angle":0,"TimeToSolve":0}


    Query SGPro for the plate solve data on an image. Starting a solve will return a GUID. Use this GUID to ask SGPro for the solve data to your image. 
    If the solve data is not yet avaiable, "Message" will contain "Solving" and "TimeToSolve" will convey how long the solve has been in progress (in seconds).
    The following routes are available for this service: 
        All Verbs /solveresults/{Receipt} 

    http://localhost:59590/solveresults?format=json
*/

namespace SGProTexter.SgProAPI
{
    internal class SgGetSolvedImageData
    {

        public SgGetSolvedImageDataResponse PtrResponse = new SgGetSolvedImageDataResponse();
        public SgGetSolvedImageDataPlaceHolder PtrPlaceHolder = new SgGetSolvedImageDataPlaceHolder();

        public SgGetSolvedImageDataResponse GetSolvedImageData(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetSolvedImageData.GetSolvedImageData()::(Caller->" + WhoCalled + ")\n"));
            DoGetSolvedImageData(WhoCalled);
            return PtrResponse;
        }

        private void DoGetSolvedImageData(string WhoCalled)
        {
            string aWhoCalled = "SgGetSolvedImageData.GetSolvedImageData().DoGetSolvedImageData()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetSolvedImageDataResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Ra:          \t" + PtrResponse.Ra + "\n");
                Trace.Write("\t\t\t Dec:         \t" + PtrResponse.Dec + "\n");
                Trace.Write("\t\t\t Scale:       \t" + PtrResponse.Scale + "\n");
                Trace.Write("\t\t\t Angle:       \t" + PtrResponse.Angle + "\n");
                Trace.Write("\t\t\t TimeToSolve: \t" + PtrResponse.TimeToSolve + "\n");
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetSolvedImageData.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Ra:          \t" + PtrResponse.Ra + "\n");
            Console.Write("\t\t\t Dec:         \t" + PtrResponse.Dec + "\n");
            Console.Write("\t\t\t Scale:       \t" + PtrResponse.Scale + "\n");
            Console.Write("\t\t\t Angle:       \t" + PtrResponse.Angle + "\n");
            Console.Write("\t\t\t TimeToSolve: \t" + PtrResponse.TimeToSolve + "\n");
        }

    }

    /*
    
    Responses POST

    All Verbs /solveresults/{Receipt} 

    POST /json/reply/SgGetSolvedImageData HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Receipt":"00000000000000000000000000000000"}

    */

    internal class SgGetSolvedImageDataPlaceHolder : SgBasePlaceHolder
    {

        public string SbVerbPath = "/guiderinfo";
    }

    /*
    
    Request GET

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String","Ra":0,"Dec":0,"Scale":0,"Angle":0,"TimeToSolve":0}

    */

    internal class SgGetSolvedImageDataResponse : SgBaseResponse
    {
        public float Ra { get; set; }
        public float Dec { get; set; }
        public float Scale { get; set; }
        public float Angle { get; set; }
        public float TimeToSolve { get; set; }
    }
}