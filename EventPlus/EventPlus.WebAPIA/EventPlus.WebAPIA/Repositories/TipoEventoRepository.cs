using EventPlus.WebAPIA.BdContectEvent;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;

namespace EventPlus.WebAPIA.Repositories;

public class TipoEventoRepository : ITipoEventoRepository
{
    private readonly EventContext _context;

    public TipoEventoRepository(EventContext context)
    {

        _context = context;     
    }
    /// <summary>
    /// Atualiza um tipo de evento usando o rastreamento automático
    /// </summary>
    /// <param name="id">id do tipo evento a ser atulizado</param>
    /// <param name="tipoEvento">Novos dados do tipo evento</param>
    public void Atualizar(Guid id, TipoEvento tipoEvento)
    {
        var tipoEventoBuscado = _context.TipoEventos.Find(id);

        if (tipoEventoBuscado != null)
        {
            tipoEventoBuscado.Titulo = tipoEvento.Titulo;

               _context.SaveChanges();

        }
    }

    

    /// <summary>
    /// Busca um tipo de evento por id 
    /// </summary>
    /// <param name="id">id do tipo evento a ser buscado</param>
    /// <returns>Objeto do tipoEvento com as informações do tipo evento buscado</returns>

    public TipoEvento BuscarPorId(Guid id)
    {
        return _context.TipoEventos.Find(id)!;
    }

    /// <summary>
    /// Cadastra um novo tipo de evento
    /// </summary>
    /// <param name="tipoEvento">Tipo de Evento a ser cadastrado</param>
    public void Cadastrar(TipoEvento tipoEvento)
    {
        _context.TipoEventos.Add(tipoEvento);
        _context.SaveChanges(); 
    }

    

    /// <summary>
    /// Deleta um Tipo de evento 
    /// </summary>
    /// <param name="id">id do tipo evento a ser deletado</param>
    public void Deletar(Guid id)
    {
        var tipoEventoBuscado = _context.TipoEventos.Find(id);
        if(tipoEventoBuscado != null)
        {

            _context.TipoEventos.Remove(tipoEventoBuscado);
            _context.SaveChanges();

        }
    }
    /// <summary>
    /// Busca a lista de tipo de eventos cadastrados
    /// </summary>
    /// <returns>lista de tipo </returns>

    public List<TipoEvento> Listar()
    {
        return _context.TipoEventos
            .OrderBy(tipoEvento => tipoEvento.Titulo)
            .ToList();
    }

    
}
