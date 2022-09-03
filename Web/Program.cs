using Web.Services;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews()
    // .AddSessionStateTempDataProvider() // 上傳圖片需要使用這個
    .AddJsonOptions(options => {
        // options.JsonSerializerOptions.PropertyNamingPolicy = null;                                  //Json回傳參數名稱按照model的大小寫
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; //Json回傳參數null不回傳
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(
            UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);                          //使用預設編碼器
        options.JsonSerializerOptions.WriteIndented = false;                                        //是否取消使用縮減JSON回傳
        options.JsonSerializerOptions.AllowTrailingCommas = true;                                   //是否允許多的","
    });

// 呼叫 Api
builder.Services.AddHttpClient<RequestService>();
string apiHost = builder.Configuration.GetSection("Api:BaseUrl").Value;
RequestService.SetApiHost(apiHost);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
