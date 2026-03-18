using EventPlus.WebAPIA.Repositories;
using EventPlus.WebAPIA.BdContectEvent;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repository;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;
    public EventoRepository(EventContext context)
    {
        _context = context;
    }
    public void Atualizar(Guid id, Evento evento)
    {
        var Evento = _context.Eventos.Find(id);

        if (Evento != null)
        {
            Evento.Nome = evento.Nome;

            _context.SaveChanges();
        }
    }


    public Evento buscar(Guid id)
    {
        return _context.Eventos.Find(id)!;
    }
 


    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

   

    public void Deletar(Guid id)
    {
        var Evento = _context.Eventos.Find(id);

        if (Evento != null)
        {
            _context.Eventos.Remove(Evento);
            _context.SaveChanges();
        }
    }

     
    
    /// <summary>
    /// Método que lista eventos filtrando pelas presenças de um USER
    /// </summary>
    /// <param name="IdUsuario">Id do Usuario para filtragem</param>
    /// <returns>Lista de eventos filtrados por usuario</returns>
  
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true))
            .ToList();
    }


    Evento IEventoRepository.BuscarPorId(Guid Id)
    {
        return _context.Eventos.FirstOrDefault(e => e.IdEvento == Id)!;
    }

    List<Evento> IEventoRepository.Listar()
    {
        return _context.Eventos.ToList();
    }
    /// <summary>
    /// Método que busca os proximos eventos que irão acontecer
    /// </summary>
    /// <returns>Lista de proximos eventos</returns>
    List<Evento> IEventoRepository.ListarProximos()
    {
        return _context.Eventos
             .Include(e => e.IdTipoEventoNavigation)
             .Include(e => e.IdInstituicaoNavigation)
             .Where(e => e.DataEvento >= DateTime.Now)
             .OrderBy(e => e.DataEvento)
             .ToList();
    }
}
