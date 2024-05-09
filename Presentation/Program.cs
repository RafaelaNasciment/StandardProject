using api_project_infrastructure.CosmosClient;
using api_project_service.Services.ProductService;
using Microsoft.OpenApi.Models;
using api_project_domain.Entity;
using api_project_infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

ConfiguracoesGlobais _configuracoesGlobais = new();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(setup =>
{
    setup.EnableAnnotations();
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = $"Standard Project - API (ambiente: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")})", Version = "v1" });
});

CosmosClientFactory cosmosClientFactory = new(_configuracoesGlobais.ConnectionString);
cosmosClientFactory.CreateAsync(GetContainersDatabase()).GetAwaiter().GetResult();

InjecaoDependencia();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void InjecaoDependencia()
{
    #region [Injeção de dependência]
    //Config
    builder.Services.AddSingleton<ConfiguracoesGlobais, ConfiguracoesGlobais>();



    //Services
    builder.Services.AddSingleton<CreateProductService, CreateProductService>();


    //Repositorio
    #endregion
}

static List<(string, string)> GetContainersDatabase()
{
    return
    [
        (nameof(Produto), nameof(Produto.IdRegistro)),
    ];
}
