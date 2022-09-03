using Api.BusinessModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
    public partial class UserService : IUserService
    {
        public async Task<UserCheckExistResponseModel> CheckExist(UserCheckExistRequestModel request)
        {
            var user = await _db.Users
                .Where(x => x.Account == request.Account && x.Email == request.Email && x.Status != (int)UserStatus.delete)
                .Select(x => new UserInfo()
                {
                    Uid = x.Id,
                    Account = x.Account,
                    Name = x.Name,
                    Email = x.Email,
                    Status = ((UserStatus)x.Status).ToString(),
                    Role = ((UserRole)x.Role).ToString(),
                })
                .FirstOrDefaultAsync();

            if(user is not null)
                return new() { Result = "fail", Message = "使用者已存在" };
            return new() { Result = "success" };
        }
    }
}

