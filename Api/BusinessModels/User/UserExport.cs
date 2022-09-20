using Api.BusinessModels.SharedModels;
using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.UserModels
{
    public class UserExportRequestModel : RequestModelsBase
    {
        [Required]
        public int Uid { get; set; }
    }

    public class UserExportResponseModel : ModelBase
    {
        public FileInfo? File { get; set; }
    }

    public class FileInfo
    {
        public string File_name { get; set; } = string.Empty;
        public string File_type { get; set; } = string.Empty;
        public byte[] File_stream { get; set; } = new byte[] {};
    }
}