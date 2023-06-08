namespace AIF.Dtos
{
    public class GoogleLoginDto
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string GoogleToken { get; internal set; }
    }
}