using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// ��������֤����
builder.Services.AddAuthentication(options =>
    {
        // ����Ĭ�ϵ������֤����ΪCookie�����֤����
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        // ����Ĭ�ϵ���ս����Ϊoidc
        options.DefaultChallengeScheme = "oidc";
    })
    // ���Cookie�����֤����
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    // ���OpenIdConnect�����֤����
    .AddOpenIdConnect("oidc", options =>
    {
        // ������Ȩ��������ַ
        options.Authority = "https://localhost:5001";
        // ���ÿͻ���ID
        options.ClientId = "web";
        // ���ÿͻ�����Կ
        options.ClientSecret = "secret";
        // ������Ӧ����
        options.ResponseType = "code";
        // ���Ĭ�ϵ�������
        options.Scope.Clear();
        // ���������
        options.Scope.Add("openid");
        options.Scope.Add("profile");

        #region ��Ӹ�����֤
        //options.Scope.Add("verification");
        //options.ClaimActions.MapJsonKey("email_verified", "email_verified");

        #endregion
        // ���û���Ϣ�˵��ȡ���� 
        options.GetClaimsFromUserInfoEndpoint=true;
        // ��ӳ�䴫�������
        options.MapInboundClaims = false;
        // ��������
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
