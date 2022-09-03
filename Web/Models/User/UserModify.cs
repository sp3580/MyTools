namespace Web.Models.User
{
    public class UserModifyRequestModel
    {
        public int Uid { get; set; }
        public int User_id { get; set; }
        public string? Name { get; set; }
        public string Account { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string? Status { get; set; }
        public string? Role { get; set; }
        public int? Profile { get; set; }
    }

    public class UserModifyResponseModel : Web.Services.ResponseService.Response
    {
    }
}