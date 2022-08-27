using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using Web.Models;

namespace Web.Services
{
    public class RequestService
    {
        // public string ApiHost { get; set; }
        public class MessagesData
        {
            public string? result { get; set; }
            public string? message { get; set; }
        }
        public HttpClient httpClient { get; }
        public RequestService(HttpClient client)
        {
            client.BaseAddress = new Uri(GlobalClient.ApiHost ?? "");
            httpClient = client;
        }
        public static void SetApiHost(string apiHost)
        {
            GlobalClient.ApiHost = apiHost;
            // this.ApiHost = apiHost;
        }
        // public bool GetVersion()
        // {
        //     GlobalClient.ApiVersion = "v1";
        //     return true;
        // }

        public async Task<string> Post(string APIcontroller, string APImethod, string device_info, object requestParams)
        {

            WebRequest request = WebRequest.Create(GlobalClient.ApiHost + "/" + APIcontroller);
            // WebRequest request = WebRequest.Create(this.ApiHost + "/" + APIcontroller);
            request.Method = "POST";

            Dictionary<string, object> json_data = new Dictionary<string, object>()
            {
                { "method", APImethod },
                { "params", requestParams }
            };

            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(json_data, settings);

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            Console.WriteLine($"{ APIcontroller } {postData}");

            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            request.Headers.Add("X-device-info", "Web+" + device_info);

            //取得資料流程
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            //string s =System.Text.Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            dataStream.Close();

            try
            {
              //response 成功回來的訊息
                WebResponse response = await request.GetResponseAsync();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                string responseFromServer;
                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }
                response.Close();

                //response 回來的Json
                return responseFromServer;
            }
            catch(System.Exception)
            {
                var responseMessage = new MessagesData()
                {
                   result = "fail",
                   message = "連線失敗"
                };

                string responseFromServer = JsonSerializer.Serialize(responseMessage);
                return responseFromServer;
            }
        }


        public async Task<string> Post(string APIcontroller, string APImethod, string device_info, string token, object requestParams)
        {
            WebRequest request = WebRequest.Create(GlobalClient.ApiHost + "/" + APIcontroller);
            request.Method = "POST";

            string params_string = Newtonsoft.Json.JsonConvert.SerializeObject(requestParams);
            var param = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(params_string);

            Dictionary<string, object> json_data = new Dictionary<string, object>()
            {
                { "method", APImethod },
                { "params", param ?? new() }
            };

            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(json_data, settings);

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            Console.WriteLine($"{ APIcontroller } {postData}");

            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            request.Headers.Add("X-device-info", "Web+" + device_info);
            request.Headers.Add("X-token", token);

            //取得資料流程
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            //string s =System.Text.Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            dataStream.Close();


            //response 成功回來的訊息
            WebResponse response = await request.GetResponseAsync();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            string responseFromServer;
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }
            response.Close();

            //response 回來的Json
            return responseFromServer;
        }


        public async Task<HttpResponseMessage> FileRequest(string APIcontroller, string APImethod, string token, string device_info, object requestParams)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(GlobalClient.ApiHost ?? "");
            // httpClient.BaseAddress = new Uri(this.ApiHost);

            string params_string = Newtonsoft.Json.JsonConvert.SerializeObject(requestParams);
            var param = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(params_string);

            Dictionary<string, object> json_data = new Dictionary<string, object>()
            {
                { "method", APImethod },
                { "params", param ?? new() }
            };
            string json_data_String = Newtonsoft.Json.JsonConvert.SerializeObject(json_data);

            HttpContent postContent = new StringContent(json_data_String, Encoding.UTF8, "application/json");
            postContent.Headers.Add("X-token", token);
            postContent.Headers.Add("X-device-info", "Web+" + device_info);

            HttpResponseMessage response = await httpClient.PostAsync(APIcontroller, postContent);

            return response;
        }

    }


}