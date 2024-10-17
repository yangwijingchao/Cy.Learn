using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
List<string> list = new List<string>();
list.Add("恭喜您获得《苍穹飞仙》游戏礼包");
list.Add("苍穹飞仙礼包");
list.Add("灵宝令:10;炼制碎片:50;强化石:100;");
list.Add("永久");
list.Add("礼包码可在游戏中输入领取，您也可以在手机上体验此游戏，点击详情查看\u2193\u2193");
var json = Newtonsoft.Json.JsonConvert.SerializeObject(list);

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
