using Duende.IdentityServer.Models;

namespace Cy.IdentityServer
{
    public static class Config
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
            }
        };


    }
}
