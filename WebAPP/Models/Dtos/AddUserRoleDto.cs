namespace WebAPP.Models.Dtos
{
    public class AddUserRoleDto
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}