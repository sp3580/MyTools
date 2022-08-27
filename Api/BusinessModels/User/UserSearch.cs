using Api.BusinessModels.SharedModels;
using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.UserModels
{
    public class UserSearchRequestModel : RequestModelsBase
    {
        // [Required]
        // public string Account { get; set; } = string.Empty;
        // [Required]
        // public string Pwd { get; set; } = string.Empty;
    }

    public class UserSearchResponseModel : ModelBase
    {
        public List<UserInfo>? List { get; set; }
    }

}