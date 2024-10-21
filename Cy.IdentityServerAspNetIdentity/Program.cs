using Cy.IdentityServerAspNetIdentity;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices().ConfigurePipeline();


app.Run();
