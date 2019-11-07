namespace TrainingPlanner.Core.Utils
{
    public static class DictionaryResources
    {
        public const string DbConnection = "DbConnection";
        public const string AllowAllHeaders = "AllowAllHeaders";

        public const string PasswordLength = "Password must contain between 6 and 30 characters";

        public const string AccountExists = "Account with this email already exists";
        public const string InvalidRegistrationAttempt = "Invalid registration attempt";
        public const string InvalidLoginAttempt = "Invalid login attempt";
        public const string NoUser = "There's no user with such email";
        public const string InvalidToken = "User token is invalid";
        public const string InvalidSendAttempt = "Invalid send attempt";
        public const string EmailNotConfirmed = "Email not confirmed";
        public const string WrongEmail = "Provided email address doesn't match yours";
        public const string InvalidChangePasswordAttempt = "Invalid change password attempt";

        public const string Facebook = "FACEBOOK";

        public const string EmailConfirmation = "Join Training Planner";
        public const string PasswordReset = "Reset Password - Training Planner";
        public const string Message = "!<br/>You're only one step from being able to log in on our website! Simply click on the link below to confirm your account:<br/>";
        public const string Thanks = "<br/>Thanks,<br/>Training Planner Team";
        public const string PasswordResetMessage = "!<br/>You've receiving this email because we received a password reset request for your account. Click the link below to reset your password.<br/>";
        public const string PasswordResetThanks = "<br/>If you didn't request a password reset, please ignore this email or reply to let us know.<br/>Thanks,<br/>Training Planner Team";

    }
}
