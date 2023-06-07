using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace AIF.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore] public string Password { get; set; }
    }
}