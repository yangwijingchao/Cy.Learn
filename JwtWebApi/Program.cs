using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// ��������֤����
builder.Services.AddAuthentication()
    // ���JWT�����֤
    .AddJwtBearer(options =>
    {
        // ������Ȩ��������ַ
        options.Authority = "https://localhost:5001";
        // ����֤����
        options.TokenValidationParameters.ValidateAudience = false;
    });
// �����Ȩ����
builder.Services.AddAuthorization(options =>
{
    // ���һ����ΪApiScope�Ĳ���
    options.AddPolicy("ApiScope", policy =>
    {
        // Ҫ���û�����֤
        policy.RequireAuthenticatedUser();
        // Ҫ���û�����api1��scope
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
