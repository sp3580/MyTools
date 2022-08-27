using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Reflection;

namespace Web.Services
{
    public class ResponseService
    {
        public ResponseService()
        {

        }
        public class Response
        {
            public string? result { get; set; }
            public string? message { get; set; }
        }

        public Response GetResponse(string responseString)
        {
            Response obj = JsonSerializer.Deserialize<Response>(responseString) ?? GetErrorValue<Response>("get response fail");
            return obj;
        }

        public TValue GetErrorValue<TValue>(string errorMsg)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(new Response()
            {
                result = "fail",
                message = errorMsg
            });
            TValue result = Newtonsoft.Json.JsonConvert.DeserializeObject<TValue>(data);
            return result;
        }

        public TValue GetData<TValue>(string responseString)
        {
            try
            {
                TValue result = Newtonsoft.Json.JsonConvert.DeserializeObject<TValue>(responseString) ?? GetErrorValue<TValue>("get response fail");
                return result;
            }
            catch (System.Exception ex)
            {
                return GetErrorValue<TValue>(ex.Message);
            }
        }
    }
}