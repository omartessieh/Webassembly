using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models.Dtos
{
    public class LoginDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}