// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using IdentityModel.Client;

Console.WriteLine("Hello, World!");
// 创建HttpClient对象
var client = new HttpClient();
// 获取OpenID Connect Discovery文档
var discoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
// 如果获取文档出错
if (discoveryDocument.IsError)
{
    // 输出错误信息
    Console.WriteLine(discoveryDocument.Error);
    // 输出异常信息
    Console.WriteLine(discoveryDocument.Exception);
    // 返回
    return ;
}
// 使用Client Credentials模式请求令牌
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    // 令牌端点
    Address = discoveryDocument.TokenEndpoint,
    // 客户端ID
    ClientId = "client2",
    // 客户端密钥
    ClientSecret = "secret",
    // 请求的API范围
    Scope = "api2"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.WriteLine(tokenResponse.Exception);
    return ;
}

Console.WriteLine(tokenResponse.AccessToken);


// 设置客户端的访问令牌
client.SetBearerToken(tokenResponse.AccessToken);
// 发送GET请求，获取https://localhost:6001/identity的响应
//使用client2访问Forbidden
var response = await client.GetAsync("https://localhost:6001/identity");
//var response = await client.GetAsync("https://localhost:6001/WeatherForecast");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
    return ;
}

var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
Console.WriteLine(JsonSerializer.Serialize(doc,new JsonSerializerOptions{WriteIndented = true}));
