using Api.BusinessModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
    public partial class UserService : IUserService
    {
        public async Task<UserSearchResponseModel> Search(UserSearchRequestModel request)
        {
            var list = await _db.Users
                .Select(x => new UserInfo()
                {
                    Uid = x.Id,
                    Account = x.Account,
                    Name = x.Name,
                    Email = x.Email,
                    Status = ((UserStatus)x.Status).ToString(),
                    Role = ((UserRole)x.Role).ToString(),
                })
                .ToListAsync();

            return new() { Result = "success", List = list };
        }
    }
}

