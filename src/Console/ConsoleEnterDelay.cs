using System;
using System.Text;

namespace SGProTexter
{
    internal class ConsoleEnterDelay
    {
        private EmailCredentialStructure ptrEmailCredentialStructure;
        public void SetEmailCredentialStructure(EmailCredentialStructure aEmailCredential)
        {
            ptrEmailCredentialStructure = aEmailCredential;
        }

        public EmailCredentialStructure GetEmailCredentialStructure()
        {
            return ptrEmailCredentialStructure;
        }

        public void EnterDelayWhileCameraWarmsUpConsole()
        {
            bool doLoop = true;

            // Get sender From
            while (doLoop)
            {
                Console.Write("\nEnter delay while camera warms up (0 = no delay, 0-900 sec): ");
                ptrEmailCredentialStructure.WarmUpDelay = Console.ReadLine();
                if (ptrEmailCredentialStructure.WarmUpDelay.Length > 0)
                {
                    doLoop = false;
                }
            }
        }

    }
}
