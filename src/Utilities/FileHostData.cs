using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SGProTexter
{
    public static class FileHostData
    {
        public const int DisplayCurrentEmailCredentialStructureInt = 1;
        public const int CreateNewEmailCredentialInt = 2;
        public const int DeleteNewEmailCredentialInt = 3;
        public const int DisplaySGProStatusInt = 4;
        public const int DeleteEmailCredentialStructureInt = 5;
        public const int RunSilientModeInt = 7;
        public const int SilientModeInt = 8;
        public const int GetEmailCredentialStructureInt = 25;
        public const int RunConsoleMainMenuInt = 55;
        public const int ExitToSystemInt = 99;

        public const string SGproURL = "http://localhost:59590";
        public const string SGproFolder = "SGProTexter";
        public const string EmailCredentialFileName = "SGProProfile.txt";
        public const string SGproLoggingErrorFileName = "SGProTextingLogging.txt";
        public static string SGproLoggingTimeFileName = CreateDateLoggingFileName();
        public static readonly string FullPathFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), FileHostData.SGproFolder);
        public static readonly string FullPathFolderEmailCredentialFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), FileHostData.SGproFolder, FileHostData.EmailCredentialFileName);
        public static readonly string FullPathFolderEmailLoggingErrorFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), FileHostData.SGproFolder, FileHostData.SGproLoggingTimeFileName);

        public static string GetLoggingTime(string aMessage)
        {
            string moment = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss.fff");
            string ReturnTime = moment + "| " + aMessage;
            return ReturnTime;
        }

        public static string CreateDateLoggingFileName()
        {

            string FileName;
            string Date = DateTime.Now.ToString("yyyy-dd-M--HH-mm");
            FileName = Date + "-" + SGproLoggingErrorFileName;
            return FileName;
        }

        public static void RemoveOldLoggingFiles()
        {
            int fileCount = Directory.GetFiles(FullPathFolder, "*" + SGproLoggingErrorFileName, SearchOption.TopDirectoryOnly).Length;

            if (fileCount > 20)
            {
                string[] FileArray = Directory.GetFiles(FullPathFolder, "*" + SGproLoggingErrorFileName, SearchOption.TopDirectoryOnly);

                for (int counter = 0; counter < 5 && FileArray.Length > 0; counter++)
                {
                    string logFile = FileArray[counter];
                    try
                    {
                        File.Delete(logFile);
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(FileHostData.GetLoggingTime("FileHostData.RemoveOldLoggingFiles() " + ex.ToString()));
                        Console.WriteLine(ex.ToString());
                        return;
                    }
                }
            }
            return;
        }

    }
}
