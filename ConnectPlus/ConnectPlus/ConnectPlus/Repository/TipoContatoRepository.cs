using ConnectPlus.BdContectConnect;
using ConnectPlus.Interface;
using ConnectPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus.Repository;

public class TipoContatoRepository : ITipoContatoRepository
{
    private readonly ConnectContext _context;
    public TipoContatoRepository(ConnectContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza um tipo de evento usando o rastreamento automatico
    /// </summary>
    /// <param name="Id"> o id do tipo evento a ser atualizada</param>
    /// <param name="tipoEvento"> novos dados do tipo evento</param>
    public void Atualizar(Guid Id, TipoContato tipoContato)
    {
        var tipoEventoBuscado = _context.TipoContatos.Find(Id);

        if (tipoContato != null)
        {
            tipoEventoBuscado.Titulo = tipoContato.Titulo;

            //savechanges detecta mudanca na propiedade "titulo" automaticamente
            _context.SaveChanges();
        }
    }

    public TipoContato BuscadoPorId(Guid id)
    {
        return _context.TipoContatos.Find(id)!;
    }




    /// <summary>
    /// Cadastra um novo tipo de evento
    /// </summary>
    /// <param name="tipoEvento">O tipo de evento a ser cadastrado</param>
    public void Cadastrar(TipoContato tipoContato)
    {
        _context.TipoContatos.Add(tipoContato);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um tipo de evento 
    /// </summary>
    /// <param name="Id">Ele recebe um tipo de evento a ser deletado</param>
    public void Deletar(Guid Id)
    {
        var tipoContatoBuscado = _context.TipoContatos.Find(Id);

        if (tipoContatoBuscado != null)
        {
            _context.TipoContatos.Remove(tipoContatoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca a lista de tipo eventos
    /// </summary>

    /// <returns>Uma lista de tipo contato</returns>
    public List<TipoContato> List()
    {
        return _context.TipoContatos.OrderBy(tipoContato => tipoContato.Titulo).ToList();
    }


}
