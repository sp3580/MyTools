using Api.Models;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // 回傳參數 null 不回傳
    options.JsonSerializerOptions.WriteIndented = false;                                        // 是否取消使用縮減 JSON 回傳
    options.JsonSerializerOptions.AllowTrailingCommas = true;                                   // 是否允許多的","
    //options.JsonSerializerOptions.IgnoreNullValues = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BlogContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

// DI Service
builder.Services.AddScoped<Api.BusinessModels.UserModels.IUserService, UserService>();
builder.Services.AddScoped<Api.BusinessModels.UploadModels.IUploadService, UploadService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
