namespace TrainingPlanner.Core.Options
{
    public class EmailOptions
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string FrontUrl { get; set; }
        public string ErrorFrontUrl { get; set; }
    }
}
