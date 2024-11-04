namespace WebAPP.Models.Dtos
{
    public class UserRoleViewDto
    {
        public long Id { get; set; }
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}