namespace Web.Models.User
{
    public class UserCheckExistRequestModel
    {
        public int Uid { get; set; }
        public string Account { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class UserCheckExistResponseModel : Web.Services.ResponseService.Response
    {
    }
}