using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace Cy.IdentityServer
{
    public static class Config3
    {


        // 返回一个ApiScope的集合
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            // 创建一个ApiScope对象，并设置其名称和描述
            //Claim
            new ApiScope("api1", "My API"),
            new ApiScope("api2", "My API2")
        };

        // 返回一个包含Client对象的IEnumerable集合
        public static IEnumerable<Client> Clients => new Client[]
        {
            // 创建一个Client对象
            new Client
            {
                // 设置ClientId为"client"
                ClientId = "client",
                // 设置ClientSecrets为"secret"的SHA256哈希值
                ClientSecrets = {new Secret("secret".Sha256())},
                // 设置AllowedGrantTypes为ClientCredentials
                AllowedGrantTypes = GrantTypes.ClientCredentials,//客戶端
                // 设置AllowedScopes为"api1"
                AllowedScopes = { "api1" }
            },
            new Client
            {
                // 设置ClientId为"client"
                ClientId = "client2",
                // 设置ClientSecrets为"secret"的SHA256哈希值
                ClientSecrets = {new Secret("secret".Sha256())},
                // 设置AllowedGrantTypes为ClientCredentials
                AllowedGrantTypes = GrantTypes.ClientCredentials,//客戶端
                // 设置AllowedScopes为"api1"
                AllowedScopes = { "api2" }
            },
            //4.配置OIDC客户端
            // 创建一个新的Client对象
            new Client
            {
                // 设置ClientId为"web"
                ClientId = "web",
                // 设置ClientSecrets为"secret"的SHA256哈希值
                ClientSecrets = {new Secret("secret".Sha256())},
                // 设置AllowedGrantTypes为Code
                AllowedGrantTypes = GrantTypes.Code,
                // 设置RedirectUris为"https://localhost:5002/signin-oidc"
                RedirectUris = { "https://localhost:5002/signin-oidc" },
                // 设置PostLogoutRedirectUris为"https://localhost:5002/signout-callback-oidc"
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                // 设置AllowedScopes为IdentityServerConstants.StandardScopes.OpenId和IdentityServerConstants.StandardScopes.Profile
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                // 设置RequireConsent为true是否支持授权操作页面与 options.GetClaimsFromUserInfoEndpoint=true;结合使用
                RequireConsent = true
            }
        };

        //2.配置OIDC范围
        // 返回一个IdentityResource类型的IEnumerable集合
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            // 创建一个IdentityResources.OpenId类型的IdentityResource对象
            new IdentityResources.OpenId(),
            // 创建一个IdentityResources.Profile类型的IdentityResource对象
            new IdentityResources.Profile(),
            // 创建一个新的IdentityResource对象
            new IdentityResource()
            {
                // 设置Name属性为"verification"
                Name = "verification",
                // 设置UserClaims属性为一个包含JwtClaimTypes.Email和JwtClaimTypes.EmailVerified的List<string>
                UserClaims = new List<string>()
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                }
            }
        };
        // 定义一个静态属性，返回一个TestUser类型的列表
        //3.测试用户
        public static List<TestUser> TestUsers => new List<TestUser>
        {
            // 创建一个TestUser对象，并设置其属性
            new TestUser
            {
                SubjectId = "1", // 设置SubjectId属性为"1"
                Username = "admin", // 设置Username属性为"admin"
                Password = "123456" // 设置Password属性为"123456"
            }
        };
    }
}
