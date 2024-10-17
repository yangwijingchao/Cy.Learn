// 创建一个Web应用程序的构建器

using Cy.IdentityServer;

var builder = WebApplication.CreateBuilder(args);
//客户端许可
//var app = builder.ConfigureServices()
//    .ConfigurePipeline();
//OIDC
var app = builder.ConfigureServices2()
    .ConfigurePipeline2();



// 运行应用程序
app.Run();