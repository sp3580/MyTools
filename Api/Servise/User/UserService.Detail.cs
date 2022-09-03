using Api.BusinessModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
    public partial class UserService : IUserService
    {
        public async Task<UserDetailResponseModel> Detail(UserDetailRequestModel request)
        {
            var user = await _db.Users
                .Where(x => x.Id == request.User_id && x.Status != (int)UserStatus.delete)
                .Select(x => new UserInfo()
                {
                    Uid = x.Id,
                    Account = x.Account,
                    Name = x.Name,
                    Email = x.Email,
                    Status = ((UserStatus)x.Status).ToString(),
                    Role = ((UserRole)x.Role).ToString(),
                    Create_time = x.CreateTime,
                    Update_time = x.UpdateTime
                })
                .FirstOrDefaultAsync();

            if(user == null)
                return new() { Result = "fail", Message = "使用者不存在" };

            return new() { Result = "success", Info = user };
        }
    }
}

