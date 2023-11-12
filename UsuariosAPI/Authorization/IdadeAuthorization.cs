using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UsuariosAPI.Authorization;

public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
{
    /// <summary>
    /// Para que possamos interceptar as requisições a fim de validar os 
    /// campos de um token, precisamos criar um handler.
    /// </summary>
    /// <param name="context">AuthorizationHandlerContext</param>
    /// <param name="requirement">IdadeMinima</param>
    /// <returns></returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
    {
        Claim? dataNascimentoClaim = context.User
            .FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

        if (dataNascimentoClaim is null) return Task.CompletedTask;

        DateTime dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);

        int idadeUsuario = DateTime.Today.Year - dataNascimento.Year;

        if (dataNascimento > DateTime.Today.AddYears(-idadeUsuario)) idadeUsuario--;

        if (idadeUsuario >= requirement.Idade) context.Succeed(requirement);
        
        return Task.CompletedTask;  
    }
}
