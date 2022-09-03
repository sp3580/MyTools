using Api.Utilities;
using Api.BusinessModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
    public partial class UserService : IUserService
    {
        public async Task<UserModifyResponseModel>Modify(UserModifyRequestModel request)
        {
            var user = await _db.Users
                .Where(x => x.Id == request.User_id && x.Email == request.Email && x.Status != (int)UserStatus.delete).FirstOrDefaultAsync();

            if(user == null)
                return new() { Result = "fail", Message = "使用者不存在" };

            // 檢查 Uid 是否為管理員，管理員可更改所有人資料，一般使用者只能更改自己資料
            var Check = _db.Users.CheckPermission(request.Uid);
            if(!Check && (user.Role == (int)UserRole.manage || user.Id != request.Uid))
                return new() { Result = "fail", Message = "您沒有權限更改該使用者資料" };

            if(!string.IsNullOrEmpty(request.Role))
            {
                user.Role = (int)Enum.Parse(typeof(UserRole), request.Role, true);
            }
            if(!string.IsNullOrEmpty(request.Status) && request.Status != (UserStatus.delete).ToString())
            {
                user.Status = (int)Enum.Parse(typeof(UserStatus), request.Status, true);
            }
            user.Name = request.Name ?? user.Name;

            if(await _db.SaveChangesAsync() == 0)
                return new() { Result = "fail", Message = "資料無變更" };

            user.UpdateTime = DateTime.Now;
            await _db.SaveChangesAsync();

            return new() { Result = "success" };
        }
    }
}

