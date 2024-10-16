// 创建一个Web应用程序的构建器

using Cy.IdentityServer;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices()
    .ConfigurePipeline();





// 运行应用程序
app.Run();