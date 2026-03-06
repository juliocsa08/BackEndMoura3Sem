using EventPlus.WebAPIA.Models;

namespace EventPlus.WebAPIA.Interfaces;

public interface IUsuarioRepository
{

    void Cadastrar(Usuario usuario);

    Usuario BuscarPorId(Guid IdUsuario);
    Usuario BuscarPorEmailESenha(string Email, string Senha);
    Usuario BuscarPorEmailESenha(string Email, string Senha);

}
