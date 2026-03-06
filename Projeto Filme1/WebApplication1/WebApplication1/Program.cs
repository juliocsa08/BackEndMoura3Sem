using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System;
using WebApplication1.BdContectFilme;
using WebApplication1.Interface;
using WebApplication1.Interfaces;
using WebApplication1.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<FilmeContext>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString
    ("DefaultConnection")));

builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();

builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {

            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            IssuerSigningKey = new SymmetricSecurityKey
            (System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),

            ClockSkew = TimeSpan.FromMinutes(5),

            ValidIssuer = "api_filmes",

            ValidAudience = "api_filmes",
        };



    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(Options =>
{

Options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
{
    Version = "v1",
    Title = "Filmes API",
    Description = "Uma API com um catálogo de filmes",
    TermsOfService = new Uri("https://exemplo.com/terms"),
    Contact = new Microsoft.OpenApi.OpenApiContact
    {
       
        Name = "GitHub Profissional",
        Url = new Uri("https://github.com/juliocsa08")
    },
    License = new Microsoft.OpenApi.OpenApiLicense
    {
        Name = "Example License",
        Url = new Uri("https://github.com/License")

    }



});

    Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {

        Name = "Autorizathion",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT:"

    });
    Options.AddSecurityRequirement(documento => new OpenApiSecurityRequirement
    {

        [new OpenApiSecuritySchemeReference("Bearer", documento)] = Array.Empty<string>().ToList(),

    });

 });

builder.Services.AddCors(Options =>

{
    Options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader();



    });
});

builder.Services.AddControllers();

var app = builder.Build();
if (app.Environment.IsDevelopment())

{
    app.UseSwagger(Options => { });

    app.UseSwaggerUI(Options => {
        Options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        Options.RoutePrefix = string.Empty;
    
    
    
    
    });

}
app.UseCors("CorsPolicy");


app.UseStaticFiles();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
