namespace TrainingPlanner.Core.Options
{
    public class EmailOptions
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public string ConfirmUrl { get; set; }
        public string ResetUrl { get; set; }
        public string ConfirmSuccessUrl { get; set; }
        public string ConfirmErrorUrl { get; set; }
        public string UserErrorUrl { get; set; }
    }
}
