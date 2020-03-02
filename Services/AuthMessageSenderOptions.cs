namespace PortfolioPage.Services{
    public class AuthMessageSenderOptions
    {

        // Use this to set default values:
        /*public AuthMessageSenderOptions()
        {
            // Set default value.
            SendingUserName = "value1_from_ctor";
        }*/
        public string RPAGE_SMTP_SendingUserName { get; set; }
        public string RPAGE_SMTP_SendingUserEmail { get; set; }
        public string RPAGE_SMTP_SendingUserPassword { get; set; }
        public string RPAGE_SMTP_SMTPdomain { get; set; }
    }
}