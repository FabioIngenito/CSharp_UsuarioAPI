using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Models;

namespace UsuariosAPI.Data;

/// <summary>
/// Para integrarmos o Identity com o Entity, devemos utilizar novamente um Context 
/// para acessar o banco. Diferente da abordagem tradicional, onde estendemos da classe 
/// DbContext, precisamos estender de outra classe para o funcionamento do Identity, o 
/// IdentityDbContext.
/// 
/// Essa classe é responsável por criar um DbContext específico para o Identity.
/// </summary>
public class UsuarioDBContext : IdentityDbContext<Usuario>
{
    public UsuarioDBContext(DbContextOptions<UsuarioDBContext> opts) : base(opts)
    {
        
    }
}
