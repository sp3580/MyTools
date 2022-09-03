using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using Web.Models.User;
using Web.ViewModels.User;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ResponseService _responseService { get; set; }
        private RequestService _requestService { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, RequestService requestService,
                IHttpContextAccessor httpContextAccessor
        )
        {
            _logger = logger;
            _responseService = new ResponseService();
            _requestService = requestService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Account, string Pwd)
        {
            if(string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(Pwd))
            {
                TempData["Alert"] = "帳號與密碼不得為空";
                return View("index");
            }

            var response_string = await _requestService.Post("user", "login", HttpContext.Request.Headers["User-Agent"], new UserLoginRequestModel(){
                Account = Account,
                Pwd = Pwd
            });
            var response_data = _responseService.GetData<UserLoginResponseModel>(response_string);
            if(response_data.result != "success")
            {
                TempData["Alert"] = response_data.message;
                return View("index");
            }

            HttpContext.Session.SetInt32("Uid", response_data.Info.Uid);
            HttpContext.Session.SetString("Name", response_data.Info.Name);
            HttpContext.Session.SetString("Account", response_data.Info.Account);
            HttpContext.Session.SetString("Token", response_data.Info.Role);
            HttpContext.Session.SetString("Role", response_data.Info.Token);
            HttpContext.Session.SetInt32("isLogin", 1);

            TempData["Success"] = "登入成功";
            return View("index");
        }

        [HttpGet]
        [SessionService.Check_Session_Filter]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}