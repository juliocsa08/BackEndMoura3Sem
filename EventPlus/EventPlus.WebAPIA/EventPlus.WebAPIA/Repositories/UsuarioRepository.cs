using EventPlus.WebAPIA.BdContectEvent;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;
using EventPlus.WebAPIA.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPIA.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;
    public UsuarioRepository(EventContext eventContext)
    {

        _context = eventContext;   
    }
    /// <summary>
    /// Busca o Usuario Pelo E-mail e valida o hash da senha 
    /// </summary>
    /// <param name="Email">Email do usuario</param>
    /// <param name="Senha">Seha do usuario</param>
    /// <returns>Usuario Buscado e validado</returns>
    public Usuario BuscarPorEmailESenha(string Email, string Senha)
    {
        var usuarioBuscado = _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.Email == Email);
        if (usuarioBuscado != null)
        {

          bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

            if (confere)
            {
                return usuarioBuscado;

            }
        }

        return null;

   
    }
    /// <summary>
    /// Busca um usuario pelo id, incluindo os dados do seu tipo usuário
    /// </summary>
    /// <param name="IdUsuario">Id do usuario a ser buscado </param>
    /// <returns>Usuario Buscado</returns>
    public Usuario BuscarPorId(Guid IdUsuario)
    {
        return _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(Usuario => Usuario.IdUsuario == IdUsuario)!;
    }
    /// <summary>
    /// Cadastra um novo Usuario com a senha Criptografada
    /// </summary>
    /// <param name="usuario">Usuario a ser Cadastradado</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);

        _context.Usuarios .Add(usuario);
        _context.SaveChanges();
    }
}
