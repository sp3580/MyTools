namespace Web.Models.User
{
    public class UserDetailRequestModel
    {
        public int Uid { get; set; }
        public int User_id { get; set; }
    }

    public class UserDetailResponseModel : Web.Services.ResponseService.Response
    {
        public UserInfo? Info { get; set; }
    }
}