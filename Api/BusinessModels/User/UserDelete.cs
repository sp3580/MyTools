using Api.BusinessModels.SharedModels;
using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.UserModels
{
    public class UserDeleteRequestModel : RequestModelsBase
    {
        [Required]
        public int Uid { get; set; }
        [Required]
        public int User_id { get; set; }
    }

    public class UserDeleteResponseModel : ModelBase
    {
    }
}