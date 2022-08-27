using Api.BusinessModels.SharedModels;
using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.UserModels
{
    public class UserLoginRequestModel : RequestModelsBase
    {
        [Required]
        public string Account { get; set; } = string.Empty;
        [Required]
        public string Pwd { get; set; } = string.Empty;
    }

    public class UserLoginResponseModel : ModelBase
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
        public string Role { get; set; } = String.Empty;
    }
}