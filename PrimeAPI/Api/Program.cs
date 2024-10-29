using CrossCutting.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Configuração de Aplicação
// -----------------------------

var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json");

// -----------------------------
// Configuração de CORS
// -----------------------------

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();  // Permite qualquer origem, método e cabeçalho
}));

// -----------------------------
// Configuração de Serviços
// -----------------------------

builder.Services.AddControllers();  // Adiciona suporte a controladores
builder.Services.AddEndpointsApiExplorer(); // Habilita a exploração de endpoints para documentação

// -----------------------------
// Configuração do Swagger
// -----------------------------

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = "Primal_Api.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // Caminho para o arquivo XML
    c.IncludeXmlComments(xmlPath); // Inclui comentários XML no Swagger

    // Define as informações da API no Swagger
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Primal",  // Título da API
        Version = "v0.1",  // Versão da API
        Description = "Primal - MVP"  // Descrição da API
    });

    // Definição de segurança para autenticação Bearer
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = @"Authentication with JWT. Ex: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer"
    });

    // Requisitos de segurança para os endpoints
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// -----------------------------
// Configuração de Autenticação
// -----------------------------

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dhfjkasfdsafdsfsahdfjkahsdjkfahsdjfhajksdfhakjfhksdfhadksjhfadkjfalsdfjkfjdfkal"));
builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata = false;  // Configurações adicionais para validação do token
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = false,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});



// -----------------------------
// Configuração de Serialização JSON
// -----------------------------

builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;  // Ignora valores nulos na serialização JSON
});


// -----------------------------
// Configuração de Serviços Personalizados
// -----------------------------

builder.Services.AddInfrastruture(builder.Configuration); // Adiciona infraestrutura personalizada
builder.Services.AddServices(builder.Configuration); // Adiciona serviços personalizados

// -----------------------------
// Configuração do Pipeline de Requisições
// -----------------------------

var app = builder.Build();

// Configura o pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita o Swagger apenas em ambiente de desenvolvimento
    app.UseSwaggerUI(); // Habilita a interface do Swagger
}

app.UseHttpsRedirection(); // Redireciona requisições HTTP para HTTPS
app.UseRouting();  // Configura o roteamento da aplicação
app.UseCors();  // Habilita o middleware de CORS
app.UseAuthentication(); // Habilita a autenticação
app.UseAuthorization(); // Habilita a autorização

app.MapControllers(); // Mapeia os controladores

// Executa a aplicação
app.Run();
