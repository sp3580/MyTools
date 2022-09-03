using Api.BusinessModels.SharedModels;
using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.UserModels
{
    public class UserCheckExistRequestModel : RequestModelsBase
    {
        [Required]
        public int Uid { get; set; }
        [Required]
        public string Account { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
    }

    public class UserCheckExistResponseModel : ModelBase
    {
    }
}