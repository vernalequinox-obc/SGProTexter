using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgGetGuiderImage

    http://localhost:59590/json/reply/SgGetGuiderImage?format=json
    results: {"Message":"No image found for receipt: 00000000-0000-0000-0000-000000000000","Success":false}
    
    After starting guider image capture, you will use this call in order to determine when the image is complete.
    The following routes are available for this service: All Verbs /guiderimagedata/{Receipt}   
    Parameters: Name Parameter Data Type Required Description 
    Receipt path int No The receipt given to you when the image capture was started. 

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback

 */

namespace SGProTexter.SgProAPI
{
    internal class SgGetGuiderImage
    {

        public static SgGetGuiderImageResponse PtrResponse = new SgGetGuiderImageResponse();
        public static SgGetGuiderImagePlaceHolder PtrPlaceHolder = new SgGetGuiderImagePlaceHolder();

        public SgGetGuiderImageResponse GetGuiderImage(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetGuiderImage.GetGuiderImage()::(Caller->" + WhoCalled + ")\n"));
            DoGetGuiderImage(WhoCalled);
            return PtrResponse;
        }

        private void DoGetGuiderImage(string WhoCalled)
        {
            string aWhoCalled = "SgGetGuiderImage.GetGuiderImage().DoGetGuiderImage()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetGuiderImageResponse>(response.Content);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetGuiderImage.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }
    }

    /* 
    Responses POST

    All Verbs /guiderimagedata/{Receipt}   

    POST /json/reply/SgGetGuiderImage HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Receipt":"00000000000000000000000000000000"}

    Example: Enter into a browser 
    http://localhost:59590/guiderimagedata/
    Results: {"Success":false,"Message":"Unable to query temp comp, focuser is not connected.","TempCompAvailable":false,"TempCompActive":false}


    */
    internal class SgGetGuiderImagePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/guiderimagedata";
        public Guid Receipt { get; set; }
    }

    /*
     Request GET

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length
    
    {"ImageData":{"ImageData":[0],"Width":0,"Height":0},"Message":"String","Success":false}

    */
    internal class SgGetGuiderImageResponse : SgBaseResponse
    {

    }
}
