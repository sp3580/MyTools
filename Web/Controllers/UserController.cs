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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User_Session _user_Session = new();

        public UserController(ILogger<UserController> logger, RequestService requestService,
                IHttpContextAccessor httpContextAccessor
        )
        {
            _logger = logger;
            _responseService = new ResponseService();
            _requestService = requestService;
            _httpContextAccessor = httpContextAccessor;
        }

        [SessionService.Check_Session_Filter]
        public async Task<IActionResult> Index()
        {
            try
            {
                _user_Session = SessionService.Get_Session(HttpContext);
            }
            catch (System.Exception)
            {
                TempData["Alert"] = "獲取登入者資訊失敗";
                return View();
            }

            var response_string = await _requestService.Post("user", "search", HttpContext.Request.Headers["User-Agent"], new UserSearchRequestModel(){

            });
            var response_data = _responseService.GetData<UserSearchResponseModel>(response_string);
            if(response_data.result != "success")
            {
                TempData["Alert"] += "\n" + response_data.message;
                return View();
            }

            TempData["Uid"] = _user_Session.Uid;
            TempData["Role"] = _user_Session.Role;

            var view_data = new UserViewModel()
            {
                SearchData = response_data
            };
            return View(view_data);
        }

        [SessionService.Check_Session_Filter]
        public IActionResult Add()
        {
            return View("Add");
        }
        [HttpPost]

        [SessionService.Check_Session_Filter]
        public async Task<IActionResult> Add([Bind(Prefix = "UserCreateRequestModel")] UserCreateRequestModel request)
        {
            if(request == null)
            {
                TempData["Alert"] = "資料不得為空";
                return RedirectToAction("Index");
            }

            try
            {
                _user_Session = SessionService.Get_Session(HttpContext);
            }
            catch (System.Exception)
            {
                TempData["Alert"] = "獲取登入者資訊失敗";
                return RedirectToAction();
            }

            request.Uid = _user_Session.Uid;

            var response_string = await _requestService.Post("user", "create", HttpContext.Request.Headers["User-Agent"], request);
            var response_data = _responseService.GetData<UserCreateResponseModel>(response_string);

            if(response_data.result != "success")
            {
                TempData["Alert"] = response_data.message;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Detail", new { User_id = response_data.Uid } );
        }

        [SessionService.Check_Session_Filter]
        public async Task<ActionResult> CheckAccount(string Account, string Email)
        {
            try
            {
                _user_Session = SessionService.Get_Session(HttpContext);
            }
            catch (System.Exception)
            {
                TempData["Alert"] = "獲取登入者資訊失敗";
                return RedirectToAction("Index");
            }

            var response_string = await _requestService.Post("user", "check_exist", HttpContext.Request.Headers["User-Agent"], new UserCheckExistRequestModel(){
                Uid = _user_Session.Uid,
                Account = Account,
                Email = Email,
            });
            var response_data = _responseService.GetData<UserCheckExistResponseModel>(response_string);
            return Json(response_data);
        }

        [SessionService.Check_Session_Filter]
        public async Task<ActionResult> Delete(int User_id)
        {
            try
            {
                _user_Session = SessionService.Get_Session(HttpContext);
            }
            catch (System.Exception)
            {
                TempData["Alert"] = "獲取登入者資訊失敗";
                return RedirectToAction("Index");
            }

            if(_user_Session.Role != "manage")
            {
                return Json(new { result = "fail", message = "您沒有權限刪除該使用者"});
            }
            var response_string = await _requestService.Post("user", "delete", HttpContext.Request.Headers["User-Agent"], new UserDeleteRequestModel(){
                Uid = _user_Session.Uid,
                User_id = User_id
            });
            var response_data = _responseService.GetData<UserDeleteResponseModel>(response_string);
            return Json(response_data);
        }

        [SessionService.Check_Session_Filter]
        public async Task<IActionResult> Detail(int User_id)
        {
            try
            {
                _user_Session = SessionService.Get_Session(HttpContext);
            }
            catch (System.Exception)
            {
                TempData["Alert"] = "獲取登入者資訊失敗";
                return RedirectToAction("Index");
            }

            var response_string = await _requestService.Post("user", "detail", HttpContext.Request.Headers["User-Agent"], new UserDetailRequestModel(){
                Uid = _user_Session.Uid,
                User_id = User_id,
            });
            var response_data = _responseService.GetData<UserDetailResponseModel>(response_string);

            if(response_data == null)
            {
                TempData["Alert"] = "查無資料";
                return RedirectToAction("Index");
            }

            if(response_data.result != "success")
            {
                TempData["Alert"] = response_data.message;
                return RedirectToAction("Index");
            }

            var view_data = new UserViewModel()
            {
                DetailData = response_data
            };

            return View("Detail", view_data);
        }

        [SessionService.Check_Session_Filter]
        public async Task<IActionResult> Edit(int User_id)
        {
            try
            {
                _user_Session = SessionService.Get_Session(HttpContext);
            }
            catch (System.Exception)
            {
                TempData["Alert"] = "獲取登入者資訊失敗";
                return RedirectToAction("Index");
            }

            var response_string = await _requestService.Post("user", "detail", HttpContext.Request.Headers["User-Agent"], new UserDetailRequestModel(){
                Uid = _user_Session.Uid,
                User_id = User_id,
            });

            var response_data = _responseService.GetData<UserDetailResponseModel>(response_string);

            if(response_data.result != "success")
            {
                TempData["Alert"] = response_data.message;
                return RedirectToAction("Index");
            }

            if(response_data == null)
            {
                TempData["Alert"] = "查無資料";
                return RedirectToAction("Index");
            }

            response_data.Info.Uid = User_id;

            var view_data = new UserViewModel()
            {
                DetailData = response_data,
                ModifyRequest = new()
                {
                    User_id = response_data.Info.Uid,
                    Name = response_data.Info.Name,
                    Account = response_data.Info.Account,
                    Email = response_data.Info.Email,
                    Role = response_data.Info.Role,
                    Status = response_data.Info.Status,
                }
            };

            return View("Edit", view_data);
        }

        [HttpPost]

        [SessionService.Check_Session_Filter]
        public async Task<IActionResult> Edit([Bind(Prefix = "ModifyRequest")] UserModifyRequestModel request)
        {
            if(request == null)
            {
                TempData["Alert"] = "資料不得為空";
                return RedirectToAction("Index");
            }

            try
            {
                _user_Session = SessionService.Get_Session(HttpContext);
            }
            catch (System.Exception)
            {
                TempData["Alert"] = "獲取登入者資訊失敗";
                return RedirectToAction("Index");
            }

            request.Uid = _user_Session.Uid;

            var response_string = await _requestService.Post("user", "modify", HttpContext.Request.Headers["User-Agent"], request);
            var response_data = _responseService.GetData<UserModifyResponseModel>(response_string);

            if(response_data.result != "success")
            {
                TempData["Alert"] = response_data.message;
                return RedirectToAction("Index");
            }

            TempData["Uid"] = _user_Session.Uid;

            return RedirectToAction("Detail", new { User_id = request.User_id } );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [SessionService.Check_Session_Filter]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [SessionService.Check_Session_Filter]
        public async Task<IActionResult> Export()
        {
            try
            {
                _user_Session = SessionService.Get_Session(HttpContext);
            }
            catch (System.Exception)
            {
                TempData["Alert"] = "獲取登入者資訊失敗";
                return RedirectToAction("Index");
            }

            var response_string = await _requestService.Post("user", "export", HttpContext.Request.Headers["User-Agent"], new UserExportRequestModel(){
                Uid = _user_Session.Uid,
            });
            var response_data = _responseService.GetData<UserExportResponseModel>(response_string);

            if(response_data == null)
            {
                TempData["Alert"] = "查無資料";
                return RedirectToAction("Index");
            }

            if(response_data.result != "success")
            {
                TempData["Alert"] = response_data.message;
                return RedirectToAction("Index");
            }

            return File(response_data.File.File_stream, response_data.File.File_type, response_data.File.File_name);
        }
    }
}