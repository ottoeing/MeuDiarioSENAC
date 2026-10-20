using MeuDiarioSENAC.Service;
using MeuDiarioSENAC.Model;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var jwtSettings = builder.Configuration.GetSection("JWTSettings");
var chaveSecreta = jwtSettings["SecretKey"]
    ?? throw new InvalidOperationException("A configuração JWTSettings:SecretKey é obrigatória.");
var chaveSecretaBytes = Encoding.ASCII.GetBytes(chaveSecreta);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
   options.RequireHttpsMetadata = false;
   options.SaveToken = true;
   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(chaveSecretaBytes),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login/auth", ([FromBody] Usuario usuario) =>
{
    TokenService authService = new TokenService(builder.Configuration);
    return authService.GerarToken(usuario);
});



var registrosGroup = app.MapGroup("/registros").RequireAuthorization();


var usuariosGroup = app.MapGroup("/usuarios");

app.MapGet("/", () => "Hello World!");

app.MapGet("/motivacional", () => "Lembre-se: todo mês tem boleto!");

registrosGroup.MapGet("/", () =>
{
    List<Registro> registros = new RegistroService().Listar();
    return registros;
});

registrosGroup.MapPost("/", ([FromBody] Registro registro) =>
{
    new RegistroService().InserirRegistro(registro);
    return "Registro cadastrado com sucesso!";
});

registrosGroup.MapDelete("/", ([FromBody] Registro registro) =>
{
    new RegistroService().RemoverPorUsuario(registro.UsuarioId, registro.Id);
    return "Registro removido com sucesso!";
});

registrosGroup.MapPut("/", ([FromBody] Registro registro) =>
{
    new RegistroService().AtualizarRegistro(registro);
    return "Registro atualizado com sucesso!";
});

usuariosGroup.MapPost("/registrar", ([FromBody] Usuario usuario) =>
{
    var usuarioCriado = new UsuarioService().Registrar(usuario.Nome, usuario.Email, usuario.Senha);

    if (usuarioCriado is null)
    {
        return Results.BadRequest(new { mensagem = "Não foi possível registrar o usuário." });
    }

    return Results.Ok(usuarioCriado);
});

usuariosGroup.MapPost("/login", ([FromBody] Usuario usuario) =>
{
    var usuarioAutenticado = new UsuarioService().Autenticar(usuario.Email, usuario.Senha);

    if (usuarioAutenticado is null)
    {
        return Results.BadRequest(new { mensagem = "Credenciais inválidas." });
    }

    return Results.Ok(usuarioAutenticado);
});

usuariosGroup.MapGet("/{usuarioId}/registros", (int usuarioId) =>
{
    return new RegistroService().ListarPorUsuario(usuarioId);
});

app.Run();

