using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class UserRoleView
    {
        [Key]
        public long Id { get; set; }
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}