using EventPlus.WebAPIA.BdContectEvent;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPIA.Repositories;

public class InstituiçãoRepository : IinstituiçãoRepository
{
    private readonly EventContext _context;

    public InstituiçãoRepository(EventContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid Id, Instituição instituicao)
    {
        var instituicaoBuscada = _context.Instituiçãos.Find(Id);

        if (instituicaoBuscada != null)
        {
            instituicaoBuscada.NomeFantasia = instituicao.NomeFantasia;
            instituicaoBuscada.Cnpj = instituicao.Cnpj;
            instituicaoBuscada.Endereço = instituicao.Endereço;

            _context.SaveChanges();

        }
    }


    public Instituição BuscarPorId(Guid id)
    {
        return _context.Instituiçãos.Find(id)!;
    }

    public void Cadastrar(Instituição instituicao)
    {
        _context.Instituiçãos.Add(instituicao);
        _context.SaveChanges();
    }


    public void Deletar(Guid id)
    {
        var instituicaoBuscada = _context.Instituiçãos.Find(id);

        if (instituicaoBuscada != null)
        {
            _context.Instituiçãos.Remove(instituicaoBuscada);
            _context.SaveChanges();
        }
    }

    public List<Instituição> Listar()
    {
        return _context.Instituiçãos
            .OrderBy(instituicao => instituicao.NomeFantasia)
            .ToList();
    }
}
