using ConnectPlus.BdContectConnect;
using ConnectPlus.Interface;
using ConnectPlus.Models;

namespace ConnectPlus.Repository;

public class ContatoRepository : IContatoRepository
{

    private readonly ConnectContext _context;

    public ContatoRepository(ConnectContext context)
    {
        _context = context;
    }
    public void Atualizar(Guid id, Contato contato)
    {
        var contatoBuscado = _context.Contatos.Find(id);

        if (contatoBuscado != null)
        {
            contatoBuscado.Nome = contato.Nome;
            contatoBuscado.FormaContato = contato.FormaContato;
            contatoBuscado.Imagem = contato.Imagem;
            _context.SaveChanges();
        }
    }



    public void Cadastrar(Contato contato)
    {
        _context.Contatos.Add(contato);
        _context.SaveChanges();
    }

    public void Deletar(Guid Id)
    {
        var contatoBuscado = _context.Contatos.Find(Id);

        if (contatoBuscado != null)
        {
            _context.Contatos.Remove(contatoBuscado);
            _context.SaveChanges();
        }
    }

    public List<Contato> List()
    {
        return _context.Contatos.OrderBy(Contato => Contato.Nome).ToList();
    }

    public Contato BuscadoPorId(Guid id)
    {
        return _context.Contatos.Find(id)!;
    }

}
