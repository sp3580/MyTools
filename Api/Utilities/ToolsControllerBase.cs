using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Api.Utilities
{
    public class ToolsControllerBase : ControllerBase
    {
        public T ParamsDeserialize<T>(object param) where T : class, new()
        {
            // 將傳入 param 轉成指定型別
            T request = JsonConvert.DeserializeObject<T>(param.ToString() ?? "") ?? new();
            // 重新執行驗證
            ModelState.Clear();
            if (!TryValidateModel(request))
            {
                // 獲取錯誤訊息
                List<string> msg = new();
                ModelState.Values.ToList().ForEach(
                    x =>
                    {
                        string tmp = x.Errors.Select(x => x.ErrorMessage).FirstOrDefault() ?? "";
                        if(!string.IsNullOrEmpty(tmp))
                        {
                            msg.Add(tmp);
                        }
                    }
                );
                throw new ValidationException(String.Join("\n", msg));
            }
            return request;
        }
    }
}
