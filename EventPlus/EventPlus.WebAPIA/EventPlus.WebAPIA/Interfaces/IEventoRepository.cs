using EventPlus.WebAPIA.Models;

namespace EventPlus.WebAPIA.Interfaces;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    List<Evento> Listar();
    void Deletar(Guid Id);
    void Atualizar(Guid Id,Evento evento);
    Evento BuscarPorId(Guid Id);
    List<Evento> ListarPorId(Guid Id);
    List<Evento> ListarProximos();
}
