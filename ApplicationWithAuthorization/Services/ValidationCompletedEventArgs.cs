namespace ApplicationWithAuthorization.Services
{
    public class ValidationCompletedEventArgs : EventArgs
    {
        public bool IsValidated { get; set; }
    }
}
