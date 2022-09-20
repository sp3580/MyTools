namespace Web.Models.User
{
    public class UserExportRequestModel
    {
        public int Uid { get; set; }
    }

    public class UserExportResponseModel : Web.Services.ResponseService.Response
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