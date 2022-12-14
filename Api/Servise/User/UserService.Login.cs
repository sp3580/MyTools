using Api.BusinessModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
    public partial class UserService : IUserService
    {
        public async Task<UserLoginResponseModel> Login(UserLoginRequestModel request)
        {
            var user = await _db.Users
                .Where(x => x.Account == request.Account && x.Pwd == request.Pwd && x.Status != (int)UserStatus.delete)
                .Select(x => new UserInfo()
                {
                    Uid = x.Id,
                    Account = x.Account,
                    Name = x.Name,
                    Email = x.Email,
                    Role = ((UserRole)x.Role).ToString(),
                    Status = ((UserStatus)x.Status).ToString(),
                    Token = "",
                })
                .FirstOrDefaultAsync();

            if(user == null) return new() { Result = "fail", Message = "帳號或密碼錯誤"};
            if(user.Status == (UserStatus.disabled).ToString()) return new() { Result = "fail", Message = "該帳號已停用" };

            return new() { Result = "success", Info = user };
        }
    }
}

