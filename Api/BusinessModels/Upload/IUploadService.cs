using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Api.BusinessModels.UploadModels
{
    public interface IUploadService
    {
        public Task<UploadImportUserRequestModel> ImportUser(int Uid, [FromQuery] IFormFile file);
    }
}
