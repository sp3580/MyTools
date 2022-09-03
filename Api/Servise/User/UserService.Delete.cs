using Api.Utilities;
using Api.BusinessModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
    public partial class UserService : IUserService
    {
        public async Task<UserDeleteResponseModel>Delete(UserDeleteRequestModel request)
        {
            var user = await _db.Users
                .Where(x => x.Id == request.User_id && x.Status != (int)UserStatus.delete).FirstOrDefaultAsync();

            if(user == null)
                return new() { Result = "fail", Message = "使用者不存在" };

            // 檢查 Uid 是否為管理員，管理員可刪除所有人
            var Check = _db.Users.CheckPermission(request.Uid);
            if(!Check)
                return new() { Result = "fail", Message = "您沒有權限刪除該使用者" };

            user.Status = (int)UserStatus.delete;

            if(await _db.SaveChangesAsync() == 0)
                return new() { Result = "fail", Message = "刪除失敗" };

            user.UpdateTime = DateTime.Now;
            await _db.SaveChangesAsync();

            return new() { Result = "success" };
        }
    }
}

