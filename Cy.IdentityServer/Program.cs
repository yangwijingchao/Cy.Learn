// ����һ��WebӦ�ó���Ĺ�����

using Cy.IdentityServer;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices()
    .ConfigurePipeline();





// ����Ӧ�ó���
app.Run();