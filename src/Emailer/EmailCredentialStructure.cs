using System;


namespace SGProTexter
{
    internal class EmailCredentialStructure
    {
        public bool IsEmailTestSuccesful { get; set; }
        public bool SgSGproEmailCredentialFileExist { get; set; }
        public bool SendOnlyOnRORorMountIssues { get; set; } 
        public string WarmUpDelay { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string EnableSsL { get; set; }
        public bool BoolEnableSSL { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }



        public void ResetEmailCredentialStructure()
        {
            IsEmailTestSuccesful = false;
            SgSGproEmailCredentialFileExist = false;
            SendOnlyOnRORorMountIssues = false;
            WarmUpDelay = "0";
            Host = string.Empty;
            Port = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
            EnableSsL = string.Empty;
            BoolEnableSSL = true;
            From = string.Empty;
            Recipient = string.Empty;
            Subject = string.Empty;
            Body = string.Empty;
        }

    }
}
