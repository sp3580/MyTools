namespace Web.Models.User
{
    public class UserDeleteRequestModel
    {
        public int Uid { get; set; }
        public int User_id { get; set; }
    }

    public class UserDeleteResponseModel : Web.Services.ResponseService.Response
    {
    }
}