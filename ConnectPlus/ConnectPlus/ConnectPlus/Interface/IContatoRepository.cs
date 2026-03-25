
using ConnectPlus.Models;

namespace ConnectPlus.Interface
{
    public interface IContatoRepository
    {
        void Atualizar(Guid id, Contato contatoAtualizado);
        Contato BuscadoPorId(Guid id);
        void Cadastrar(Contato novoContato);
        void Deletar(Guid id);
        List<Contato> List();
    }
}
