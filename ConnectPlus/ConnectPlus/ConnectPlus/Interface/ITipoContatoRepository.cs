using ConnectPlus.Models;

namespace ConnectPlus.Interface;

public interface ITipoContatoRepository
{

    void Cadastrar(TipoContato tipoContato);

    List<TipoContato> List();


    void Deletar(Guid Id);

    TipoContato BuscadoPorId(Guid id);

    void Atualizar(Guid id, TipoContato tipoContato);

}
