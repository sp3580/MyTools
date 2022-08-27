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
                .Where(x => x.Account == request.Account && x.Pwd == request.Pwd)
                .Select(x => new UserInfo()
                {
                    Uid = x.Id,
                    Account = x.Account,
                    Name = x.Name,
                    Email = x.Email,
                    Role = ((UserRole)x.Role).ToString(),
                    Token = "",
                })
                .FirstOrDefaultAsync();

            if(user == null) return new() { Result = "fail", Message = "Account or Password is incorrect."};

            return new() { Result = "success", Info = user };
        }
    }
}

