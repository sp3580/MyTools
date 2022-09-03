namespace Web.Models.User
{
    public class UserCreateRequestModel
    {
        public int Uid { get; set; }
        public string Account { get; set; } = String.Empty;
        public string Pwd { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
    }

    public class UserCreateResponseModel : Web.Services.ResponseService.Response
    {
        public int Uid { get; set; }
    }
}