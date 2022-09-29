using Api.Utilities;
using Api.BusinessModels.UploadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;
using System.Net;
using CsvHelper;

namespace Api.Services
{
    public partial class UploadService : IUploadService
    {
        // 允許的檔案類型
        private readonly string[] _permitted_csv = { ".csv" };
        // private readonly string[] _permitted_attachment = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".doc", ".docx", ".xls", ".xlsx", ".pdf" };
        // private readonly string[] _permitted_image = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".doc", ".docx" };
        // private readonly string[] _permitted_pdf = { ".pdf" };
        // private string[] _permitted_extensions = {};
        public async Task<UploadImportUserRequestModel>ImportUser(int Uid, [FromQuery] IFormFile File)
        {
            // 檢查 Uid 是否為管理員，管理員可刪除所有人
            var check = _db.Users.CheckPermission(Uid);
            if(!check)
                return new() { Result = "fail", Message = "您沒有權限匯出使用者" };

            if (File is null) return new UploadImportUserRequestModel() { Result = "fail", Message = "No File Found." };

            string trustFileName = WebUtility.HtmlEncode(File.FileName);

            string ext = Path.GetExtension(trustFileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !_permitted_csv.Contains(ext))
                return new() { Result = "fail", Message = "請上傳 CSV 檔案" };

            List<CsvUser> Rows = new List<CsvUser>();
            using (var MemoryStream = new MemoryStream())
            {
                await File.CopyToAsync(MemoryStream);
                MemoryStream.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(MemoryStream, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine()?.Trim() ?? "";
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] subs = line.Split(',');
                            Rows.Add(new()
                            {
                                Account = subs[0],
                                Name = subs[1],
                                Email = subs[2],
                                Status = subs[3],
                                Role = subs[4],
                                isExist = false
                            });
                        }
                    }
                }
            }
            return new() { Result = "success", Info = Rows };
        }
    }
}

