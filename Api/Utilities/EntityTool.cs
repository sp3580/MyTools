using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Api.Models;
using Api.BusinessModels.UserModels;

namespace Api.Utilities
{
    public static class EntityTool
    {
        public static bool CheckEnable(this IEnumerable<User> user, int uid, int? status=(int)UserStatus.enabled)
        {
            var IQ = user.Where(x => x.Id == uid && x.Status == status);
            return IQ.FirstOrDefault() == null ? false : true;
        }
        public static bool CheckPermission(this IEnumerable<User> user, int uid, int? role=(int)UserRole.manage)
        {
            var IQ = user.Where(x => x.Id == uid && x.Role == role);
            return IQ.FirstOrDefault() == null ? false : true;
        }
    }
}
