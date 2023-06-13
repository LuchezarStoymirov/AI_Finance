using System.Text.Json.Serialization;

namespace AIF.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore] public string Password { get; set; }
        public object FirstName { get; internal set; }
        public object UserName { get; internal set; }
        public object LastName { get; internal set; }
    }
}