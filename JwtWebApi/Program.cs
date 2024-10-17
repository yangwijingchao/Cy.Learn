using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
List<string> list = new List<string>();
list.Add("��ϲ����á������ɡ���Ϸ���");
list.Add("���������");
list.Add("�鱦��:10;������Ƭ:50;ǿ��ʯ:100;");
list.Add("����");
list.Add("����������Ϸ��������ȡ����Ҳ�������ֻ����������Ϸ���������鿴\u2193\u2193");
var json = Newtonsoft.Json.JsonConvert.SerializeObject(list);

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
