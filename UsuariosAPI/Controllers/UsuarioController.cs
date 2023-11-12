using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.DTOs;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService cadastroService)
    {
        _usuarioService = cadastroService;
    }

    /// <summary>
    /// A lógica do cadastro do usuário está encapsulado em "_cadastroService".
    /// 
    /// Quando o código da nossa lógica atual aumentar, ela acabará sendo
    /// misturada com uma nova lógica de finalidade diferente.
    /// Quando aumentarmos o código, as responsabilidades vão se misturar. 
    /// Uma alternativa para contornar tal problema é criar classes contendo 
    /// cada lógica específica.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        await _usuarioService.Cadastra(dto);

        return Ok("Usuário cadastrado!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUsuarioDto dto)
    {
        string token = await _usuarioService.Login(dto);

        return Ok(token);
    }
}
