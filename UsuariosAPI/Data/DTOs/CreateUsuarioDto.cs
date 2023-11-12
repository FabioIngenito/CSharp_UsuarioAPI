using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.DTOs;

public class CreateUsuarioDto
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public DateTime DataNascimento { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    // Essa anotação compara e avalia se dois campos são iguais.
    [Compare("Password")]
    public string? RePassword { get; set; }

}
