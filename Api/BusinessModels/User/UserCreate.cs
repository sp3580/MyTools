using Api.BusinessModels.SharedModels;
using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.UserModels
{
    public class UserCreateRequestModel : RequestModelsBase
    {
        [Required]
        public int Uid { get; set; }
        [Required]
        public string Account { get; set; } = String.Empty;
        [Required]
        public string Pwd { get; set; } = String.Empty;
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Role { get; set; } = String.Empty;
    }

    public class UserCreateResponseModel : ModelBase
    {
        public int Uid { get; set; }
    }
}