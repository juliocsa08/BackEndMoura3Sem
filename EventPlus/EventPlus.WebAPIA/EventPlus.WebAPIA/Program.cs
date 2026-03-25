using Azure.AI.ContentSafety;
using EventPlus.WebAPI.Repository;
using EventPlus.WebAPIA.BdContectEvent;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

//1.Configurar o contexto banco de dados
builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//2. Registrar as Repositories (Injeção de Dependência)
builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();

builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();

builder.Services.AddScoped<IinstituiçãoRepository, InstituiçãoRepository>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IEventoRepository, EventoRepository>();

builder.Services.AddScoped<IComentarioEventoRepository, ComentarioEventoRepository>();

//configuraçãodo azure content safety
var endpoint = "https://moderatorservice-julio.cognitiveservices.azure.com/";

var apikey = "";

var client = new ContentSafetyClient(new Uri(endpoint), new
    Azure.AzureKeyCredential(apikey));
builder.Services.AddSingleton(client);

// Adiciona Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo
{

    Version = "v1",
    Title = "Api De Eventos",
    Description = "Aplicação para gerenciamento de eventos",
    TermsOfService = new Url("https://example.com/terms"),
    Contact = new OpenApiContact
    {
        Name = "julio cesar",
        Url = new Url("https://www.linkedin.com/in/julioCesarSoaresAvelar")
    },
    License = new OpenApiLicense
    {

        Name = "Licensa de Exemplo",
        Url = new Url("https://example.com/license")
    }
});

options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Insira o token JWT:"

});

options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
{
[new OpenApiSecuritySchemeReference("Bearer", document)] =
        Array.Empty<string>().ToList()
  });

});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options => { });

    app.UseSwaggerUI(options => 
    {

        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

internal class Url : Uri
{
    public Url([StringSyntax("Uri")] string uriString) : base(uriString)
    {
    }
}