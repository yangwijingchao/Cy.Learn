// ����һ��WebӦ�ó���Ĺ�����

using Cy.IdentityServer;

var builder = WebApplication.CreateBuilder(args);
//�ͻ������
//var app = builder.ConfigureServices()
//    .ConfigurePipeline();
//OIDC
//var app = builder.ConfigureServices2()
//    .ConfigurePipeline2();

//���һ����Ϊ����֤���� ��� ��Դ�����а��� �ʼ� �� email _ verified ���⡣
var app = builder.ConfigureServices3()
    .ConfigurePipeline3();

// ����Ӧ�ó���
app.Run();