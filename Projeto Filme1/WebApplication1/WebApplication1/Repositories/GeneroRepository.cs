using WebApplication1.BdContectFilme;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class GeneroRepository : IGeneroRepository
{
    private readonly FilmeContext _context;
    public GeneroRepository(FilmeContext context)
    {
        _context = context;
    }

    public void AtualizarIdCor(Genero generoAtualizado)
    {
        try
        {
            Genero generoBuscado = _context.Generos.Find
                 (generoAtualizado.IdGenero)!;
            if (generoBuscado != null) 
            {
                generoBuscado.Nome = generoAtualizado.Nome;
        }
            _context.Generos.Update(generoBuscado!);
            _context.SaveChanges();
       }
        catch (Exception)
        {

            throw;
        }
    }

    public void AtualizarIdUrl(Guid id, Genero generoAtualizado)
    {
        try
        {
            Genero generoBuscado = _context.Generos.Find(id.ToString());
            if (generoBuscado != null)
             {
                generoBuscado.Nome = generoAtualizado.Nome;

                _context.Generos.Update(generoBuscado);
                _context.SaveChanges(); 

            }

        }
        catch (Exception ) 
        {
            throw;
        
        
        }

    }

    public Genero BuscarPorId(Guid id)
    {
       try
        {
            Genero generoBuscado = _context.Generos.Find(id.ToString())!;
            
            return generoBuscado;

        }
        catch(Exception)
        {
            throw;

        }
    }

    public void Cadastrar(Genero novoGenero)
    {
        try
        {
            novoGenero.IdGenero = Guid.NewGuid().ToString();
        _context.Generos.Add(novoGenero);
        _context.SaveChanges();
        }

        catch (Exception ex) {
            throw;
            }
    }

    public void Deletar(Guid id)
    {
        try
        {
            Genero generoBuscado = _context.Generos.Find(id.ToString())!;
            if (generoBuscado != null) {
                {
                    _context.Generos.Remove(generoBuscado);
                }
                _context.SaveChanges();
               }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public List<Genero> Listar()
    {
        try
        {
            List<Genero> ListaGeneros = _context.Generos.ToList();
            return ListaGeneros;

        }   
        catch (Exception ex)
        {
            throw;
        }
    }
}
