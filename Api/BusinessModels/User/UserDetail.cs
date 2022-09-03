using Api.BusinessModels.SharedModels;
using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.UserModels
{
    public class UserDetailRequestModel : RequestModelsBase
    {
        [Required]
        public int Uid { get; set; }
        [Required]
        public int User_id { get; set; }
    }

    public class UserDetailResponseModel : ModelBase
    {
        public UserInfo? Info { get; set; }
    }
}