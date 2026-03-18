using EventPlus.WebAPIA.Models;

namespace EventPlus.WebAPIA.Interfaces;

public interface IinstituiçãoRepository
{
    void Cadastrar(Instituição instituição);
    void Deletar(Guid id);

    List<Instituição> Listar();
    Instituição BuscarPorId(Guid id);
    void Atualizar(Guid id, Instituição instituição);
}

