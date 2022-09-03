using Api.BusinessModels.SharedModels;
using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.UserModels
{
    public class UserModifyRequestModel : RequestModelsBase
    {
        [Required]
        public int Uid { get; set; }
        [Required]
        public int User_id { get; set; }
        public string? Name { get; set; }
        public string? Pwd { get; set; }
        public string Account { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string? Status { get; set; }
        public string? Role { get; set; }
        public int? Profile { get; set; }
    }

    public class UserModifyResponseModel : ModelBase
    {
    }
}