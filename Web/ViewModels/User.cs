using Web.Models.User;

namespace Web.ViewModels.User
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            SearchData = new();
            DetailData = new();
            CreateRequest = new();
            ModifyData = new();
            ModifyRequest = new();
        }
        public UserSearchResponseModel SearchData { get; set; }
        public UserDetailResponseModel DetailData { get; set; }
        public UserCreateRequestModel CreateRequest { get; set; }
        public UserModifyRequestModel ModifyRequest { get; set; }
        public UserModifyResponseModel ModifyData { get; set; }
    }
}