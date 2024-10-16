namespace Cy.IdentityServer
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            //https://localhost:5001/connect/token
            //client_id:client
            // client_secret:secret
            // grant_type:client_credentials
            builder.Services.AddIdentityServer()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients);
            return builder.Build();

        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // 配置路由
            app.UseStaticFiles();
            app.UseRouting();

            // 配置IdentityServer
            app.UseIdentityServer();

           

            return app;
        }
    }
}
