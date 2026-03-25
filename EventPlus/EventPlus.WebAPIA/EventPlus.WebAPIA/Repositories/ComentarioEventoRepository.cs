using EventPlus.WebAPI.Models;
using EventPlus.WebAPIA.BdContectEvent;
using EventPlus.WebAPIA.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repository;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
   
    private readonly EventContext _context;

    public ComentarioEventoRepository(EventContext context)
    {
        _context = context;
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid idUsuario, Guid idEvento)
    {
        return _context.ComentarioEventos
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdEventoNavigation)
            .FirstOrDefault(c => c.IdUsuario == idUsuario && c.IdEvento == idEvento)!;
    }

    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();
    }

    public void Deletar(Guid idComentarioEvento)
    {
        var comentarioEventoBuscado = _context.ComentarioEventos.Find(idComentarioEvento);
        if (comentarioEventoBuscado != null)
        {
            _context.ComentarioEventos.Remove(comentarioEventoBuscado);
            _context.SaveChanges();
        }
    }

    public List<ComentarioEvento> List(Guid IdEvento)
    {
        throw new NotImplementedException();
    }

    public List<ComentarioEvento> Listar(Guid idEvento)
    {
        return _context.ComentarioEventos.ToList();
    }

    public List<ComentarioEvento> ListarSomenteExibe(Guid idEvento)
    {
        return _context.ComentarioEventos
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdEventoNavigation)
            .Where(c => c.IdEvento == idEvento && c.Exibe)
            .ToList();
    }
}
