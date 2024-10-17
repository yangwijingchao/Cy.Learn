using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// 添加身份验证服务
builder.Services.AddAuthentication(options =>
    {
        // 设置默认的身份验证方案为Cookie身份验证方案
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        // 设置默认的挑战方案为oidc
        options.DefaultChallengeScheme = "oidc";
    })
    // 添加Cookie身份验证方案
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    // 添加OpenIdConnect身份验证方案
    .AddOpenIdConnect("oidc", options =>
    {
        // 设置授权服务器地址
        options.Authority = "https://localhost:5001";
        // 设置客户端ID
        options.ClientId = "web";
        // 设置客户端密钥
        options.ClientSecret = "secret";
        // 设置响应类型
        options.ResponseType = "code";
        // 清空默认的作用域
        options.Scope.Clear();
        // 添加作用域
        options.Scope.Add("openid");
        options.Scope.Add("profile");

        #region 添加更多验证
        //options.Scope.Add("verification");
        //options.ClaimActions.MapJsonKey("email_verified", "email_verified");

        #endregion
        // 从用户信息端点获取声明 
        options.GetClaimsFromUserInfoEndpoint=true;
        // 不映射传入的声明
        options.MapInboundClaims = false;
        // 保存令牌
        options.SaveTokens = true;
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
