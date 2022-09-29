using Api.BusinessModels.SharedModels;
using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.UploadModels
{
    public class UploadImportUserResponseModel : RequestModelsBase
    {
        [Required]
        public int Uid { get; set; }
        [Required]
        public byte[] Stream { get; set; } = new byte[] {};
    }

    public class UploadImportUserRequestModel : ModelBase
    {
        public List<CsvUser>? Info { get; set; }
    }

    public class CsvUser
    {
        public string Account { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool isExist { get; set; }
    }
}