using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgCaptureImage

    http://localhost:59590/json/reply/SgCaptureImage
    Results:{"Success":false,"Message":"There is no camera connected!","Receipt":"00000000000000000000000000000000"}


    Ask SGPro to capture an image. This is an asynchronous operation and will return immediately. 
    The return type (SgCaptureImageResponse) will contain a GUID and this can be used to retrieve the image when it is complete (using /imagepath/{Receipt}).

    The following routes are available for this service: POST /image   

    Parameters: Name Parameter Data Type Required Description 
    BinningMode path int Yes A number from 1 to 4 where 1 means 1x1 binning; DSLR cameras use "1" 
    ExposureLength path float Yes The exposure length in decimal seconds 
    Gain path string No If the camera supports it, the gain must be set (call /cameraprops endpoint to find acceptable values; note that the camera MUST BE CONNECTED in order to get a valid gain options list). 
    Iso path string No If the camera supports it, the ISO must be set (call /cameraprops endpoint to find acceptable values). 
    Speed path string No The download speed of the camera (if supported). Values are "Normal" or "HiSpeed". 
    FrameType path string No The frame type (controls the shutter). Values are "Light", "Dark", "Bias" and "Flat". 
    Path path string No The base path where the image should be saved. Actual (full) image name will determined by SGPro and returned via /imagepath/{Receipt}. 
    UseSubframe path boolean No A boolean indicating of a subframe should be used 
    X path int No The X coordinate of the subframe. 
    Y path int No The Y coordinate of the subframe. 
    Width path int No The Width of the subframe. 
    Height path int No The Height of the subframe. 

    http://localhost:59590/json/reply/SgCaptureImage?format=json

*/

namespace SGProTexter.SgProAPI
{
    internal class SgCaptureImage
    {
        SgCaptureImageResponse PtrResponse = new SgCaptureImageResponse();
        SgCaptureImagePlaceHolder PtrPlaceHolder = new SgCaptureImagePlaceHolder();
        SgCaptureImagePostBody PtrPostBody = new SgCaptureImagePostBody();

        public SgCaptureImageResponse CaptureImage(SgCaptureImagePostBody aPostBody, string WhoCalled)
        {
            PtrPostBody = aPostBody;
            DoCaptureImage(WhoCalled);
            Trace.Write(FileHostData.GetLoggingTime("SgCaptureImage.CaptureImage()::(Caller->" + WhoCalled + ")\n"));
            return PtrResponse;
        }

        private void DoCaptureImage(string WhoCalled)
        {
            string aWhoCalled = "SgCaptureImage.CaptureImage().DoCaptureImage::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.PostPlaceHolder(PtrPlaceHolder.RequestResourceString, PtrPostBody));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgCaptureImageResponse>(response.Content);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgCaptureImage.CaptureImage().DoCaptureImage::(Caller->" + WhoCalled + ")");
        }
    }

    /*
    Responses POST
    POST /image 

    POST /json/reply/SgCaptureImage HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"BinningMode":0,"ExposureLength":0,"Gain":"String","Iso":"String","Speed":"Normal","FrameType":"Light","Path":"String","UseSubframe":false,"X":0,"Y":0,"Width":0,"Height":0}

    */

    internal class SgCaptureImagePostBody
    {
        public string BinningMode { get; set; }
        public string ExposureLength { get; set; }
        public string Gain { get; set; }
        public string Iso { get; set; }
        public string Speed { get; set; }
        public string FrameType { get; set; }
        public string Path { get; set; }
        public string UseSubframe { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }

    internal class SgCaptureImagePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/image";
    }

    /*
    
    Request GET
    
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String","Receipt":"00000000000000000000000000000000"}

    Example of output if pasting this url to a browser
    http://localhost:59590/json/reply/SgCaptureImage 
    {"Success":false,"Message":"There is no camera connected!","Receipt":"00000000000000000000000000000000"}

    */

    internal class SgCaptureImageResponse : SgBaseResponse
    {
        public Guid Receipt { get; set; }
    }
}
