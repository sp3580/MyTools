using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Api.BusinessModels.UserModels;
using Api.BusinessModels.SharedModels;
using Api.Utilities;


namespace Api.Controllers.UserController
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ToolsControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _api;
        private readonly BlogContext _db;

        public UserController(ILogger<UserController> logger, IUserService api, BlogContext db)
        {
            _logger = logger;
            _api = api;
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult> Main(RequestModel<object> request)
        {
            try
            {
                return request.Method switch
                {
                    UserMethod.Login => Ok(await _api.Login(ParamsDeserialize<UserLoginRequestModel>(request.Params))),
                    UserMethod.Search => Ok(await _api.Search(ParamsDeserialize<UserSearchRequestModel>(request.Params))),
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