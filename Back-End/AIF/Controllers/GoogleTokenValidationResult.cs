namespace AIF.Controllers
{
    public class GoogleTokenValidationResult
    {
        internal object Email;

        public bool IsValid { get; internal set; }
        public string ErrorMessage { get; internal set; }
    }
}