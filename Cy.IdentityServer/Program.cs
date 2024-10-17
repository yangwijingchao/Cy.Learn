// 创建一个Web应用程序的构建器

using Cy.IdentityServer;

var builder = WebApplication.CreateBuilder(args);
//客户端许可
//var app = builder.ConfigureServices()
//    .ConfigurePipeline();
//OIDC
//var app = builder.ConfigureServices2()
//    .ConfigurePipeline2();

//添加一个名为“验证”的 身份 资源，其中包括 邮件 和 email _ verified 索赔。
var app = builder.ConfigureServices3()
    .ConfigurePipeline3();

// 运行应用程序
app.Run();