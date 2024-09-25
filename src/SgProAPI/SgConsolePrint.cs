using System;
using System.Diagnostics;

namespace SGProTexter.SgProAPI
{
    internal class SgConsolePrint
    {
        public void ConsolePrint(SgBasePlaceHolder PtrPlaceHolder, SgBaseResponse PtrResponse, string aClassName)
        {
            if (PtrResponse != null)
            {
                string aMessage = "SgConsolePrint.ConsolePrint_Response()::(Caller->" + aClassName + ")\n"
                    + "\t Request: \t" + PtrPlaceHolder.RequestResourceString + "\n"
                    + "\t Success: \t" + PtrResponse.Success + "\n"
                    + "\t Message: \t" + PtrResponse.Message + "\n"
                    + "\t State:   \t" + PtrResponse.State + "\n";

                Console.WriteLine(aMessage);
                Trace.Write(FileHostData.GetLoggingTime(aMessage));
            }
            else
            {
                string aMessage = "SgConsolePrint.ConsolePrint_Response()::(Caller->" + aClassName + ")\n"
                    + "\t Request: \t" + PtrPlaceHolder.RequestResourceString + "\n"
                    + "\t          \t Or DevicesResponse is NULL. - SGPRO Not Running. \n";
                Console.WriteLine(aMessage);
                Trace.Write(FileHostData.GetLoggingTime(aMessage));
            }
        }
    }
}
