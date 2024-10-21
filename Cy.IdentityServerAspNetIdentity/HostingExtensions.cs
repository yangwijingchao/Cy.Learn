using System.Security.Claims;
using Cy.IdentityServerAspNetIdentity.Data;
using Cy.IdentityServerAspNetIdentity.Models;
using Duende.IdentityServer;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Cy.IdentityServerAspNetIdentity
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
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });
            // 添加身份验证服务
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                // 使用Entity Framework作为数据存储
                .AddEntityFrameworkStores<ApplicationDbContext>()
                // 添加默认的令牌提供程序
                .AddDefaultTokenProviders();
            builder.Services.AddIdentityServer()
                // 添加内存中的身份资源 2.配置OIDC范围
                .AddInMemoryIdentityResources(Config.IdentityResources)
                // 添加内存中的API范围
                .AddInMemoryApiScopes(Config.ApiScopes)
                // 添加内存中的客户端
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>()
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
            app.MapRazorPages().RequireAuthorization();
            return app;
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

                var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var admin = userMgr.FindByNameAsync("admin").Result;
                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@163.com",
                        EmailConfirmed = true,
                        FavoriteColor = "blue"
                        
                    };
                    var result = userMgr.CreateAsync(admin, "Pass123$").Result;
                    if (result.Succeeded)
                    {
                        result = userMgr.AddClaimsAsync(admin, new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, "admin"),
                            new Claim(JwtClaimTypes.GivenName, "admin"),
                            new Claim(JwtClaimTypes.FamilyName, "admin"),
                            new Claim(JwtClaimTypes.WebSite, "http://admin.com"),
                        }).Result;

                    }
                }

            }
        }
    }
}
