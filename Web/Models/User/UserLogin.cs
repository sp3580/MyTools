namespace Web.Models.User
{
    public class UserLoginRequestModel
    {
        public string Account { get; set; } = string.Empty;
        public string Pwd { get; set; } = string.Empty;
    }

    public class UserLoginResponseModel : Web.Services.ResponseService.Response
    {
        public UserInfo? Info { get; set; }
    }

    public class UserInfo
    {
        public int Uid { get; set; }
        public string Account { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Token { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
        public DateTime Create_time { get; set; }
        public DateTime Update_time { get; set; }
    }
}