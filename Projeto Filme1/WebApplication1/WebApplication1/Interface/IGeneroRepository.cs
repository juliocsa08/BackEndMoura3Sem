using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IGeneroRepository
    {
        void Cadastrar(Genero novoGenero);
        List<Genero> Listar();
        void AtualizarIdCor(Genero generoAtualizado);
        void AtualizarIdUrl(Guid id, Genero genero);
        void Deletar(Guid id);
        Genero BuscarPorId(Guid id);
    }
}
