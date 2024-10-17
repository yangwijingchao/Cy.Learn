
namespace Cy.IdentityServer
{
    public static class HostingExtensions2
    {
        public static WebApplication ConfigureServices2(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            //https://localhost:5001/connect/token
            //client_id:client
            // client_secret:secret
            // grant_type:client_credentials
            // 添加IdentityServer服务
            builder.Services.AddIdentityServer()
                // 添加内存中的身份资源 2.配置OIDC范围
                .AddInMemoryIdentityResources(Config2.IdentityResources)
                // 添加内存中的API范围
                .AddInMemoryApiScopes(Config2.ApiScopes)
                // 添加内存中的客户端
                .AddInMemoryClients(Config2.Clients)
                // 3.测试用户
                .AddTestUsers(Config2.TestUsers);
                
            return builder.Build();

        }

        public static WebApplication ConfigurePipeline2(this WebApplication app)
        {
            // 配置路由
            app.UseStaticFiles();
            app.UseRouting();

            // 配置IdentityServer
            app.UseIdentityServer();
           
           
            app.MapRazorPages();
            return app;
        }
    }
}
