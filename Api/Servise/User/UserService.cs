using Api.BusinessModels.UserModels;
using Api.Models;

namespace Api.Services
{
    public partial class UserService : IUserService
    {
        private readonly BlogContext _db;
        private readonly ILogger<UserService> _logger;

        public UserService(BlogContext db, ILogger<UserService> logger)
        {
            _db = db;
            _logger = logger;
        }
    }
}
