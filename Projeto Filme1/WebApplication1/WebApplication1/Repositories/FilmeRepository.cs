using WebApplication1.BdContectFilme;
using WebApplication1.Interface;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly FilmeContext _context;

        public  FilmeRepository( FilmeContext context)
        {

            _context = context;
        }

        public void AtualizarIdCorpo(Filme filmeAtualizado)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(filmeAtualizado.IdFilme)!;
                if (filmeBuscado != null)
                {
                    filmeBuscado.Titulo = filmeAtualizado.Titulo;
                    filmeBuscado.IdGenero = filmeAtualizado.IdGenero;
                }
                _context.Filmes.Update(filmeBuscado!);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AtualizarUrl(Guid id, Filme filmeAtualizado)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(id.ToString());
                if (filmeBuscado != null)
                {
                    filmeBuscado.Titulo = filmeAtualizado.Titulo;
                    filmeBuscado.IdGenero = filmeAtualizado.IdGenero;

                }
                _context.Filmes.Update(filmeBuscado!);
                _context.SaveChanges();
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Filme BuscarPorId(Guid id)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(id.ToString());
                return filmeBuscado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Cadastrar(Filme NovoFilme)
        {
            try
            {
                NovoFilme.IdFilme = Guid.NewGuid().ToString();

                _context.Filmes.Add(NovoFilme);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(id.ToString());
                if (filmeBuscado != null)
                {
                    _context.Filmes.Remove(filmeBuscado);

                }
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Filme> Listar()
        {
            try
            {
                List<Filme> listarFilmes = _context.Filmes.ToList();
                return listarFilmes;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
