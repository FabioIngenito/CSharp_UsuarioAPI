using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Models;

public class Usuario : IdentityUser
{
    // Adiciona um campo nosso
    public DateTime DataNascimento { get; set; }

    // Herda todos os campos do IdentyUser - superclasse base
    public Usuario() : base() { }

}
