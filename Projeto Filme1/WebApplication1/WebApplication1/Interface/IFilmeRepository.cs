using WebApplication1.Controllers;
using WebApplication1.Models;


namespace WebApplication1.Interface
{
    public interface IFilmeRepository
    {
        void Cadastrar(Filme NovoFilme);
        List<Filme> Listar();
        void AtualizarIdCorpo(Filme filmeAtualizado);
        void AtualizarUrl(Guid id, Filme filmeAtualizado);
        void Deletar(Guid id);
        Filme BuscarPorId(Guid id);
    }
}
