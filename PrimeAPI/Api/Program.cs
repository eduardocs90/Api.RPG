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
// Configura��o de Aplica��o
// -----------------------------

var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json");

// -----------------------------
// Configura��o de CORS
// -----------------------------

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();  // Permite qualquer origem, m�todo e cabe�alho
}));

// -----------------------------
// Configura��o de Servi�os
// -----------------------------

builder.Services.AddControllers();  // Adiciona suporte a controladores
builder.Services.AddEndpointsApiExplorer(); // Habilita a explora��o de endpoints para documenta��o

// -----------------------------
// Configura��o do Swagger
// -----------------------------

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = "Primal_Api.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // Caminho para o arquivo XML
    c.IncludeXmlComments(xmlPath); // Inclui coment�rios XML no Swagger

    // Define as informa��es da API no Swagger
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Primal",  // T�tulo da API
        Version = "v0.1",  // Vers�o da API
        Description = "Primal - MVP"  // Descri��o da API
    });

    // Defini��o de seguran�a para autentica��o Bearer
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = @"Authentication with JWT. Ex: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer"
    });

    // Requisitos de seguran�a para os endpoints
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
// Configura��o de Autentica��o
// -----------------------------

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dhfjkasfdsafdsfsahdfjkahsdjkfahsdjfhajksdfhakjfhksdfhadksjhfadkjfalsdfjkfjdfkal"));
builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata = false;  // Configura��es adicionais para valida��o do token
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
// Configura��o de Serializa��o JSON
// -----------------------------

builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;  // Ignora valores nulos na serializa��o JSON
});


// -----------------------------
// Configura��o de Servi�os Personalizados
// -----------------------------

builder.Services.AddInfrastruture(builder.Configuration); // Adiciona infraestrutura personalizada
builder.Services.AddServices(builder.Configuration); // Adiciona servi�os personalizados

// -----------------------------
// Configura��o do Pipeline de Requisi��es
// -----------------------------

var app = builder.Build();

// Configura o pipeline de requisi��es
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita o Swagger apenas em ambiente de desenvolvimento
    app.UseSwaggerUI(); // Habilita a interface do Swagger
}

app.UseHttpsRedirection(); // Redireciona requisi��es HTTP para HTTPS
app.UseRouting();  // Configura o roteamento da aplica��o
app.UseCors();  // Habilita o middleware de CORS
app.UseAuthentication(); // Habilita a autentica��o
app.UseAuthorization(); // Habilita a autoriza��o

app.MapControllers(); // Mapeia os controladores

// Executa a aplica��o
app.Run();
