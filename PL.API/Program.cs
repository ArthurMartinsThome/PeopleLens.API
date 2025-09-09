using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PL.Application.Interface;
using PL.Application.Service;
using PL.Infra.Util.Model;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true; // Opcional, mas boa prática para APIs
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // 1. Configurar o Swagger para suportar JWT Bearer Token
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT neste formato: Bearer SEU_TOKEN_AQUI",
    });

    // 2. Adicionar o requisito de segurança para todas as operações
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    // Opcional: Configurar para usar os comentários XML da sua controller
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // options.IncludeXmlComments(xmlPath);
});

builder.Services.Configure<EnvironmentSettings>(
    builder.Configuration.GetSection("Env"));

var environmentSettings = builder.Configuration.GetSection("Env").Get<EnvironmentSettings>();

builder.Services.AddHttpContextAccessor();

// ------------------- Configuração JWT Authentication -------------------

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = environmentSettings.JWT_Issuer,
            ValidAudience = environmentSettings.JWT_Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(environmentSettings.JWT_Key))
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Authentication failed: " + context.Exception.Message);
                if (context.Exception.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + context.Exception.InnerException.Message);
                }
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully!");
                foreach (var claim in context.Principal.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                }
                return Task.CompletedTask;
            },
            OnMessageReceived = context =>
            {
                string authHeader = context.Request.Headers["Authorization"];
                Console.WriteLine($"Authorization Header received: {authHeader}");
                if (string.IsNullOrEmpty(authHeader))
                {
                    Console.WriteLine($"authHeader: {authHeader}");
                    Console.WriteLine("Authorization header is missing.");
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

// ------------------- Injeção de dependências -------------------
// Serviços de aplicação
builder.Services.AddScoped<IUserService, UserService>();

// Fontes de dados (PostgreSQL)
builder.Services.AddScoped<PL.Adapter.PostgreSQL.Interface.IUserDataSource, PL.Adapter.PostgreSQL.DataSource.UserDataSource>();

// ---------------------------------------------------------------

builder.Services.AddCors(options =>
{
    //options.AddPolicy("AllowSpecificOrigin", // Nome da sua política
    //    policy =>
    //    {
    //        // Substitua "http://localhost:5173" pela URL exata do seu frontend React.
    //        // Se você usar "https", use https://localhost:5173
    //        // Para produção, substitua por sua URL de produção: https://seufrontend.com.br
    //        policy.WithOrigins("http://localhost:5173")
    //              .AllowAnyHeader()   // Permite todos os cabeçalhos (Authorization, Content-Type, etc.)
    //              .AllowAnyMethod();  // Permite todos os métodos HTTP (GET, POST, PUT, DELETE, OPTIONS)
    //                                  //.AllowCredentials(); // Se você estiver usando cookies ou credenciais, adicione esta linha.
    //                                  // Nota: AllowAnyOrigin + AllowCredentials NÃO é permitido.
    //    });
    // Se você tiver múltiplas origens ou cenários complexos, pode adicionar mais políticas.
    // Ou, para facilitar o desenvolvimento (não recomendado para produção):
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin() // MUITO PERMISSIVO: Qualquer origem pode acessar
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PL.Api v1");
    // Opcional: Adiciona um campo de autorização global no Swagger UI
    // c.OAuthClientId("swagger-ui");
    // c.OAuthClientSecret("swagger-ui-secret");
    // c.OAuthRealm("swagger-ui-realm");
    // c.OAuthAppName("Swagger UI");
    // c.OAuthScopeSeparator(" ");
    // c.OAuthAdditionalQueryStringParams(new { foo = "bar" });
});

#if DEBUG
app.UseHttpsRedirection();
#else
//app.UseHttpsRedirection();
#endif

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#if DEBUG
//app.Urls.Add("http://localhost:5000");
#else
app.Urls.Add("http://localhost:5000");
#endif

app.Run();