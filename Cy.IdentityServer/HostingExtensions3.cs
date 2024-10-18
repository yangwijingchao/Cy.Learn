
using Duende.IdentityServer;
using Microsoft.IdentityModel.Tokens;

namespace Cy.IdentityServer
{
    public static class HostingExtensions3
    {
        public static WebApplication ConfigureServices3(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            //https://localhost:5001/connect/token
            //client_id:client
            // client_secret:secret
            // grant_type:client_credentials
            // 添加IdentityServer服务
            // 添加身份验证服务
            builder.Services.AddAuthentication()
                // 添加Google身份验证
                .AddGoogle("Google", options =>
                {
                    // 设置登录方案
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    // 设置客户端ID
                    options.ClientId = "your client id";
                    // 设置客户端密钥
                    options.ClientSecret = "your client secret";
                })
                .AddOpenIdConnect("oidc", "IdentityServer", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;

                    options.Authority = "https://demo.duendesoftware.com";
                    options.ClientId = "interactive.confidential";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                }); 
            builder.Services.AddIdentityServer()
                // 添加内存中的身份资源 2.配置OIDC范围
                .AddInMemoryIdentityResources(Config3.IdentityResources)
                // 添加内存中的API范围
                .AddInMemoryApiScopes(Config3.ApiScopes)
                // 添加内存中的客户端
                .AddInMemoryClients(Config3.Clients)
                // 3.测试用户
                .AddTestUsers(Config3.TestUsers);

            return builder.Build();

        }

        public static WebApplication ConfigurePipeline3(this WebApplication app)
        {
            // 配置路由
            app.UseStaticFiles();
            app.UseRouting();
            // 配置IdentityServer
            app.UseIdentityServer();
            app.UseAuthorization();
            app.MapRazorPages();
            return app;
        }
    }
}
