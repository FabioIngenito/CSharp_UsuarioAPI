using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.DTOs;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services;

/// <summary>
/// Criação e Login do usuário
/// </summary>
public class UsuarioService
{
    private readonly IMapper _mapper;
    private readonly UserManager<Usuario> _UserManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly TokenService _tokenService;

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _UserManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    /// <summary>
    /// A classe UserManager provê diversos métodos voltados para a parte de 
    /// gerenciamento de usuário, inclusive cadastro.
    /// </summary>
    /// <param name="dto">dto</param>
    /// <returns></returns>
    /// <exception cref="ApplicationException">ApplicationException</exception>
    public async Task Cadastra(CreateUsuarioDto dto)
    {
        Usuario usuario = _mapper.Map<Usuario>(dto);

        IdentityResult resultado = await _UserManager.CreateAsync(usuario, dto.Password!);

        if (!resultado.Succeeded)
            throw new ApplicationException("Falha ao cadastrar o usuário!");
    }

    /// <summary>
    /// O método "PasswordSignInAsync" pertence à classe SignInManager e com ele é 
    /// possível efetuar login utilizando usuário e senha.
    /// 
    /// O método "SignInManager" nos provê acesso aos métodos de Login e recuperação
    /// de usuários.  
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="ApplicationException"></exception>
    public async Task<string> Login(LoginUsuarioDto dto)
    {
        SignInResult resultado =
            await _signInManager.PasswordSignInAsync(dto.Username!, dto.Password!, false, false);

        if (!resultado.Succeeded)
            throw new ApplicationException("Usuário NÃO autenticado!");

        Usuario? usuario = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == dto.Username!.ToUpper());

        string token = _tokenService.GenerateToken(usuario!);

        return token;
    }
}
