// ����һ��WebӦ�ó���Ĺ�����

using Cy.IdentityServer;

var builder = WebApplication.CreateBuilder(args);
//�ͻ������
//var app = builder.ConfigureServices()
//    .ConfigurePipeline();
//OIDC
var app = builder.ConfigureServices2()
    .ConfigurePipeline2();



// ����Ӧ�ó���
app.Run();