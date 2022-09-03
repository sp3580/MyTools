using Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Web.Services
{
    public class SessionService
    {
        public class Check_Session_Filter : Attribute, IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var isLogin = context.HttpContext.Session.GetInt32("isLogin") ?? 0;
                if(isLogin == 0)
                {
                    // 無法使用 TempData 傳遞，先用 ViewData 傳遞 訊息
                    // var TempData = context.HttpContext.RequestServices.GetRequiredService<ITempDataDictionary>();
                    // TempData["Alert"] = "登入逾時, 請重新登入";
                    context.Result = new ViewResult() {
                        ViewName = "~/Views/Home/index.cshtml",
                        ViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) {
                            { "Alert", "登入逾時, 請重新登入" }
                        },
                    };
                    return;
                }
                var resultContext = await next();
            }
        }
        public static User_Session Get_Session(HttpContext httpContext) {

            return new User_Session() {
                Uid = httpContext.Session.GetInt32("Uid") ?? 0,
                Account = httpContext.Session.GetString("Account"),
                Name = httpContext.Session.GetString("Name"),
                Token = httpContext.Session.GetString("Token"),
                Role = httpContext.Session.GetString("Role"),
            };
        }
    }
}