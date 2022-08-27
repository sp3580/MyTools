namespace Web.Models.User
{
    public class UserSearchRequestModel
    {
    }

    public class UserSearchResponseModel : Web.Services.ResponseService.Response
    {
        public List<UserInfo>? List { get; set; }
    }
}