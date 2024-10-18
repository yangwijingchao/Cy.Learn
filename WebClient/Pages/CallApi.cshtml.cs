using System.Net.Http.Headers;
using System.Text.Json;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class CallApiModel(IHttpClientFactory httpClientFactory ) : PageModel
    {
        public string Json = string.Empty;
        public async Task OnGet()
        {
            //var accessToken = HttpContext.GetTokenAsync("access_token");
            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Result);

            //扩展方法返回一个名为 tokenInfo 的对象，该对象包含所有存储的标记。这将确保在需要时使用刷新令牌 自动刷新 访问令牌。
            //var tokenInfo = await HttpContext.GetUserAccessTokenAsync();
            //var client = new HttpClient();
            //client.SetBearerToken(tokenInfo.AccessToken!);
            var client = httpClientFactory.CreateClient("apiClient");

            var content = await client.GetStringAsync("https://localhost:6001/identity");
            var parsed = JsonDocument.Parse(content);
            var formatted = JsonSerializer.Serialize(parsed, new JsonSerializerOptions { WriteIndented = true });
            Json = formatted;
        }
    }
}
