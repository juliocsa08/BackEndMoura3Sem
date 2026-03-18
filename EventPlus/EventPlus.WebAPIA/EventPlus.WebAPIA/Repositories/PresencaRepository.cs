using EventPlus.WebAPIA.BdContectEvent;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPIA.Repositories;

public class PresencaRepository : IPresencaRepository
{
    public readonly EventContext _context;

    public PresencaRepository(EventContext context)
    {
        _context = context; 
    }
    /// <summary>
    /// Método que altera a situação da presença
    /// </summary>
    /// <param name="id">id da presenca a ser alterada</param>
    public void Atualizar(Guid id)
    {
        var presencaBuscada = _context.Presencas.Find(id);
        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao;

            _context.SaveChanges();


        }
    }
    /// <summary>
    /// Método que busca uma presenca por id
    /// </summary>
    /// <param name="id">Id da presenca a ser buscada</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _context.Presencas
             .Include(p => p.IdEventoNavigation)
             .ThenInclude(e => e!.IdInstituicaoNavigation)
             .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id)
    {
        
        var presencaBuscada = _context.Presencas.Find(id);

        if (presencaBuscada != null)
        {
            _context.Presencas.Remove(presencaBuscada);
            _context.SaveChanges();
        }
    }

    

    public List<Presenca> Listar()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Método que lista as presença de um usuário especifico
    /// </summary>
    /// <param name="idUsuario">id do usuario para filtragem</param>
    /// <returns>lista de presenca de um usuario</returns>
    public List<Presenca> ListarMinhas(Guid idUsuario)
    {
        return _context.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == idUsuario)
            .ToList();  
    }

    public void Inscrever(Presenca presenca)
    {
        _context.Presencas.Add(presenca);
        _context.SaveChanges();
    }

    

}
