using Web.Models.User;

namespace Web.ViewModels.User
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            SearchData = new();
        }
        public UserSearchResponseModel SearchData { get; set; }
    }
}