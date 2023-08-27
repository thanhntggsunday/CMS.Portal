namespace Common.Dto
{
    public class AppAllUserPermissionDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FunctionId { get; set; }
        public string ActionId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}