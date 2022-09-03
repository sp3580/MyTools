using Api.BusinessModels.UserModels;
using Api.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
    public partial class UserService : IUserService
    {
        public async Task<UserCreateResponseModel> Create(UserCreateRequestModel request)
        {
            // 檢查 Uid 是否為管理員，管理員可新增使用者，一般使用者不行
            var Check = _db.Users.CheckPermission(request.Uid);
            if(!Check)
                return new() { Result = "fail", Message = "您沒有權限新增使用者" };

            var user = await _db.Users.Where(x => x.Account == request.Account).FirstOrDefaultAsync();
            if(user != null)
                return new() { Result = "fail", Message = "使用者已存在" };

            var entity = new Api.Models.User()
            {
                Account = request.Account,
                Pwd = request.Pwd,
                Name = request.Name,
                Email = request.Email,
                Role = (int)Enum.Parse(typeof(UserRole), request.Role, true),
                Status = (int)UserStatus.enabled,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            _db.Add(entity);

            if(await _db.SaveChangesAsync() == 0)
                return new() { Result = "fail", Message = "新增時發生錯誤" };

            return new() { Result = "success", Uid = entity.Id };
        }
    }
}

