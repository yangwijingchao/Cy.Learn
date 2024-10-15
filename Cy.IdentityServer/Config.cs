using Duende.IdentityServer.Models;

namespace Cy.IdentityServer
{
    public static class Config
    {

        //// 返回一个IdentityResource对象的集合
        //public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        //{
        //    // 创建一个IdentityResource对象，并设置其名称和描述
        //    new IdentityResources.OpenId(),
        //    new IdentityResources.Profile()
        //};

        // 返回一个ApiScope的集合
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            // 创建一个ApiScope对象，并设置其名称和描述
            new ApiScope("api1", "My API")
        };

        // 返回一个包含Client对象的IEnumerable集合
        public static IEnumerable<Client> Clients => new Client[]
        {
            // 创建一个Client对象
            new Client
            {
                // 设置ClientId为"m2m.client"
                ClientId = "m2m.client",
                // 设置ClientSecrets为"secret"的SHA256哈希值
                ClientSecrets = {new Secret("secret".Sha256())},
                // 设置AllowedGrantTypes为ClientCredentials
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // 设置AllowedScopes为"api1"
                AllowedScopes = { "api1" }
            }
        };


    }
}
