using System.Security.Claims;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddBff()
    .AddRemoteApis();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001";
        options.ClientId = "bff";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.Scope.Add("openid");
        options.Scope.Add("profile");

        options.Scope.Add("api1");
        options.Scope.Add("offline_access");
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.MapInboundClaims = false;
    });
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();


app.UseBff();

app.UseAuthorization();

app.MapBffManagementEndpoints();
app.MapGet("/local/identity", localIdentityHandler).AsBffApiEndpoint();
app.MapRemoteBffApiEndpoint("/remote", "https://localhost:6001")
    .RequireAccessToken(Duende.Bff.TokenType.User);
app.Run();
return;


[Authorize]
static IResult localIdentityHandler(ClaimsPrincipal user)
{
    var name = user.FindFirst("name")?.Value ?? user.FindFirst("sub").Value;

    return Results.Json(new { message = "Local API Success", user = name });
}