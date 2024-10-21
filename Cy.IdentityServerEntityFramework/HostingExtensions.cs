using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Cy.IdentityServerEntityFramework
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            //https://localhost:5001/connect/token
            //client_id:client
            // client_secret:secret
            // grant_type:client_credentials
            // 添加IdentityServer服务
            // 添加身份验证服务
            builder.Services.AddAuthentication();
            var migrationsAssembly = builder.Environment.ApplicationName;
            const string connectionString = "Data Source=localhost;Database=IdentityServer;User ID=root;Password=root;TreatTinyAsBoolean=true;";
            // 添加IdentityServer服务
            builder.Services.AddIdentityServer()
                // 添加配置存储
                .AddConfigurationStore(options =>
                {
                    // 配置MySQL数据库上下文
                    options.ConfigureDbContext = b => b.UseMySQL(connectionString,
                        mySqlOptionsAction => mySqlOptionsAction.MigrationsAssembly(migrationsAssembly));
                })
                // 添加操作存储
                .AddOperationalStore(options =>
                {
                    // 配置MySQL数据库上下文
                    options.ConfigureDbContext = b => b.UseMySQL(connectionString,
                        mySqlOptionsAction => mySqlOptionsAction.MigrationsAssembly(migrationsAssembly));
                })
                // 添加测试用户
                .AddTestUsers(Config.TestUsers)
                ;

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // 配置路由
            app.UseStaticFiles();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            InitializeDatabase(app);
            app.UseRouting();
            // 配置IdentityServer
            app.UseIdentityServer();
            app.UseAuthorization();
            app.MapRazorPages();
            return app;
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
