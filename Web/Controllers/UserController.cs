using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using Web.Models.User;
using Web.ViewModels.User;
using Web.Services;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private ResponseService _responseService { get; set; }
        private RequestService _requestService { get; set; }

        public UserController(ILogger<UserController> logger, RequestService requestService)
        {
            _logger = logger;
            _responseService = new ResponseService();
            _requestService = requestService;
        }

        public async Task<IActionResult> Index()
        {
            var response_string = await _requestService.Post("user", "search", HttpContext.Request.Headers["User-Agent"], new UserSearchRequestModel(){

            });
            var response_data = _responseService.GetData<UserSearchResponseModel>(response_string);
            if(response_data.result != "success")
            {
                TempData["Alert"] = response_data.message;
                return View();
            }
            var view_data = new UserViewModel()
            {
                SearchData = response_data
            };
            return View(view_data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}