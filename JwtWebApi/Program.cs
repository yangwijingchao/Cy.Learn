using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// 添加身份验证服务
builder.Services.AddAuthentication()
    // 添加JWT身份验证
    .AddJwtBearer(options =>
    {
        // 设置授权服务器地址
        options.Authority = "https://localhost:5001";
        // 不验证受众
        options.TokenValidationParameters.ValidateAudience = false;
    });
// 添加授权服务
builder.Services.AddAuthorization(options =>
{
    // 添加一个名为ApiScope的策略
    options.AddPolicy("ApiScope", policy =>
    {
        // 要求用户已认证
        policy.RequireAuthenticatedUser();
        // 要求用户具有api1的scope
        policy.RequireClaim("scope", "api1");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();
app.MapGet("identity", (ClaimsPrincipal user) => user.Claims.Select(x => new { x.Type, x.Value }))
    .RequireAuthorization("ApiScope");
app.Run();
