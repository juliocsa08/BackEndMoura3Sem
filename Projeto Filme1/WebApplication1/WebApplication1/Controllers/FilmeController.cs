using Filmes.WebAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Interface;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmeController : ControllerBase
{
    private readonly IFilmeRepository _filmeRepository;

    public FilmeController(IFilmeRepository filmeRepository)
    {
        _filmeRepository = filmeRepository;
    }
    //[Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_filmeRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }






    }
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_filmeRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }




    }



    [HttpPost]
    public async Task<IActionResult> Post([FromForm] FilmeDTO NovoFilme)
    {

        if (string.IsNullOrWhiteSpace(NovoFilme.Titulo)&& NovoFilme.IdGenero !=null)
            return BadRequest("É obrigatorio que o filme tenha nome e Gênero");

        Filme filme = new Filme();
        if(NovoFilme.Imagem!=null && NovoFilme.Imagem.Length > 0)
        {
            var extensao = Path.GetExtension(NovoFilme.Imagem.FileName).ToLower();
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);


            if(!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using(var stream = new FileStream(caminhoCompleto,FileMode.Create))
            {
                await NovoFilme.Imagem.CopyToAsync(stream); 

            }

            filme.Imagem = nomeArquivo;

        }

        filme.IdGenero = NovoFilme.IdGenero.ToString();
        filme.Titulo = NovoFilme.Titulo!;


        try
        {
            _filmeRepository.Cadastrar(filme);
            return StatusCode(201);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);

        }

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, FilmeDTO filme)
    {

        var filmeBuscado = _filmeRepository.BuscarPorId(id);
        if (filmeBuscado == null)
            return NotFound("Filme não encontrado!");

        if (!string.IsNullOrWhiteSpace(filme.Titulo))
            filmeBuscado.Titulo = filme.Titulo;
        if (filme.IdGenero != null && filme.IdGenero.ToString() != filmeBuscado.IdGenero)
          filmeBuscado.IdGenero = filme.IdGenero.ToString();

        if(filme.Imagem != null && filme.Imagem.Length != 0)
        {
            var pastaRelativa = "wwwroot/imagens"; 
          var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);



            if(!String.IsNullOrEmpty(filmeBuscado.Imagem))
            {
                var caminhoAntigo = Path.Combine(caminhoPasta, filmeBuscado.Imagem);
                if (System.IO.File.Exists(caminhoAntigo))
                   System.IO.File.Delete(caminhoAntigo);
            }
            var extensao = Path.GetExtension(filme.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
            if(!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);
            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
            using(var stream = new FileStream(caminhoCompleto,FileMode.Create))
            {
                await filme.Imagem.CopyToAsync(stream);
            }
            filmeBuscado.Imagem = nomeArquivo;
        }

        try
        {
            _filmeRepository.AtualizarUrl(id, filmeBuscado);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }

    }
    [HttpPut]
    public IActionResult PutBody(Filme filme)
    {
        try
        {
            _filmeRepository.AtualizarIdCorpo(filme);
            return NoContent();
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }



}


    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var filmeBuscado = _filmeRepository.BuscarPorId(id);
        if(filmeBuscado == null)
            return NotFound("Filme não encontrado!");
        var pastaRelativa = "wwwroot/imagens";
        var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

        if(!String.IsNullOrEmpty(filmeBuscado.Imagem))
        {
           var caminho = Path.Combine(caminhoPasta, filmeBuscado.Imagem);
            if (System.IO.File.Exists(caminho))
                System.IO.File.Delete(caminho);


        }

        try
        {
            _filmeRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
     }
}
