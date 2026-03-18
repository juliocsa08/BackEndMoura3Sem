using EventPlus.WebAPIA.Models;

namespace EventPlus.WebAPIA.Interfaces;

public interface ITipoUsuarioRepository
{
    void Cadastrar(TipoUsuario tipoUsuario);
    void Deletar(Guid id);
    List<TipoUsuario> Listar();
    TipoUsuario BuscarPorId(Guid id);
    void Atualizar(Guid id, TipoUsuario tipoUsuario);
}