using Api.Utilities;
using Api.BusinessModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace Api.Services
{
    public partial class UserService : IUserService
    {
        public async Task<UserExportResponseModel>Export(UserExportRequestModel request)
        {
            // // 檢查 Uid 是否為管理員，管理員可刪除所有人
            // var check = _db.Users.CheckPermission(request.Uid);
            // if(!check)
            //     return new() { Result = "fail", Message = "您沒有權限匯出使用者" };

            var users = await _db.Users.Where(x => x.Status != (int)UserStatus.delete).ToListAsync();
            if(users == null || users.Count == 0)
                return new() { Result = "fail", Message = "使用者不存在" };

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"帳號,姓名,信箱,狀態,權限");

            foreach(var user in users)
            {
                var status = ((UserStatus)user.Status).ToString();
                var role = ((UserRole)user.Role).ToString();
                sb.AppendLine($"{user.Account},{user.Name},{user.Email},{status},{role}");
            }
            var stream = new MemoryStream();

            using (StreamWriter sw =
                new StreamWriter(stream, Encoding.UTF8))
            {
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
            byte[] buffer = stream.ToArray();

            var file = new Api.BusinessModels.UserModels.FileInfo()
            {
                File_name = "使用者.csv",
                File_stream = buffer,
                File_type = "text/csv"
            };

            return new() { Result = "success", File = file };
        }
    }
}

