using SGProTexter.SgProAPI;

namespace SGProTexter
{
    internal class SgBaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool Enable { get; set; }
        public string State { get; set; }
        public string LogMessage { get; set; }


        public string FormatEorrorMessage(string ErrorMessage, string WhoCalled, string RequestResourceString)
        {

            Success = false;
            Message = WhoCalled + RequestResourceString + "\n\t\t\t   ";
            if (ErrorMessage != null)
            {
                string[] stringSplit = ErrorMessage.Split(' ');
                int charCounter = 0;
                for (int i = 0; i < stringSplit.Length; i++)
                {
                    Message += stringSplit[i] + " ";
                    charCounter += stringSplit[i].Length;

                    if (charCounter > 80)
                    {
                        Message += "\n\t\t\t   ";
                        charCounter = 0;
                    }
                }
            }
            Message += "\n\t\t\t   Or SGPro is not responding and may not be running.\n";
            return Message;
        }

        public string FormatLogMessage(string WhoCalled, string RequestResourceString)
        {
            LogMessage = WhoCalled + "\n"
                    + "\t\t\t Request:   \t" + RequestResourceString + "\n"
                    + "\t\t\t Success:   \t" + Success + "\n"
                    + "\t\t\t Message:   \t" + Message + "\n"
                    + "\t\t\t State:     \t" + State + "\n";
            return LogMessage;
        }
    }
}
