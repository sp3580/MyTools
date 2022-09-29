using Api.BusinessModels.UploadModels;
using Api.Models;

namespace Api.Services
{
    public partial class UploadService : IUploadService
    {
        private readonly BlogContext _db;
        private readonly ILogger<UploadService> _logger;

        public UploadService(BlogContext db, ILogger<UploadService> logger)
        {
            _db = db;
            _logger = logger;
        }
    }
}
