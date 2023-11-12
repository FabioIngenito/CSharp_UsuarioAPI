//Safe storage of app secrets in development in ASP.NET Core
//Armazenamento seguro de segredos do aplicativo em desenvolvimento no ASP.NET Core
//Article - 04/10/2023
//https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows
//https://learn.microsoft.com/pt-br/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows
//
//DETALHE, o "secrets" (Gerenciar Segredos do Usuário) :
//- NÃO embaralha os caracteres do código para evitar engenharia reversa.
//- NÃO Criptografa senhas da aplicação.
//- SOMENTE armazena informações confidenciais durante o desenvolvimento de um projeto.
//
// POWERSHELL:
// ..\UsuariosAPI\UsuariosAPI
// dotnet user-secrets init
// Incializou e colocou comandos dentro do: UsuariosAPI: "UserSecretsId"
// dotnet user-secrets set "SymmetricSecurityKey" "this is my custom Secret key for authentication"
// E colocou na pasta:
// C:\Users\SEU_USUARIO\AppData\Roaming\Microsoft\UserSecrets
// Para guardar o caminho da conexão ao banco de dados:
// dotnet user-secrets set "ConnectionStrings:UsuarioConnection" "server=localhost;port=3306;database=usuariodb;user=root;password=teste"
//
// Por meio deste comando, criaremos um secret com determinada chave e valor informados:
// Padrão: dotnet user-secrets set <chave> <valor>
// O nome do arquivo é: "secrets.json"

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;     
    }

    /// <summary>
    /// Com tokens, nosso usuário consegue comprovar sua identidade para a 
    /// nossa aplicação.
    /// Tokens são uma forma de identificar com quem estamos nos comunicando 
    /// entre usuário e aplicação.
    /// </summary>
    /// <param name="usuario"></param>
    public string GenerateToken(Usuario usuario)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", usuario.UserName!),
            new Claim("id", usuario.Id),
            new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()),
            new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        };
        
        //ERRO: "chave muito pequena" // SymmetricSecurityKey chave = new(Encoding.UTF8.GetBytes("9ASHDA98H9ah9ha9H9A89n0f"));
        //SymmetricSecurityKey chave2 = new(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
        SymmetricSecurityKey chave3 = new(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]!));

        SigningCredentials signingCredentials =
            new(chave3, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}