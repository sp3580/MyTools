using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Api.BusinessModels.UploadModels;
using Api.BusinessModels.SharedModels;
using Api.Utilities;


namespace Api.Controllers.UploadController
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ToolsControllerBase
    {
        private readonly ILogger<UploadController> _logger;
        private readonly IUploadService _api;
        private readonly BlogContext _db;

        public UploadController(ILogger<UploadController> logger, IUploadService api, BlogContext db)
        {
            _logger = logger;
            _api = api;
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult> Main(int Uid, string Method, [FromQuery] IFormFile File, string? Type)
        {
            try
            {
                if(Uid==0 || File == null) return Ok(new ResponseModel("should have valid method"));
                return Method switch
                {
                    UploadMethod.ImportUser => Ok(await _api.ImportUser(Uid, File)),
                    _ => Ok(new ResponseModel("should have valid method")),
                };
            }
            catch (System.Exception ex)
            {
                return Ok(new ResponseModel(ex.Message));
            }
        }
    }
}