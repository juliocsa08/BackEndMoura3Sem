using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPIA.DTO;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{

    private readonly ContentSafetyClient _contentSafetyClient;

    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    [HttpGet("ListarTodos{id}")]
    public IActionResult ListarEvento(Guid id)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ListarSomenteExibe{id}")]
    public IActionResult ListarSomenteExibe(Guid id)
    {
        return Ok(_comentarioEventoRepository.ListarSomenteExibe(id));
    }

    [HttpGet("BuscarPorIdUsuario{id}")]
    public IActionResult BuscarPorIdUsuario(Guid idUsuario, Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.BuscarPorIdUsuario(idUsuario, idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }




    [HttpPost]
    public async Task<IActionResult> Post(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("O texto a ser moderado não pode estar vazio");
            }

            //Criar objeto de análise
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);


            // Chamar a API do Azure Content Safety
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);


            //Verificar se o texto tem alguma severidade maior que 0
            bool temConteudoImpropio = response.Value.CategoriesAnalysis.Any(c => c.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                IdEvento = comentarioEvento.IdEvento,
                IdUsuario = comentarioEvento.IdUsuario,
                Descricao = comentarioEvento.Descricao,
                Exibe = !temConteudoImpropio,
                DataComentarioEvento = DateTime.Now

            };

            _comentarioEventoRepository.Cadastrar(novoComentario);

            return StatusCode(201, novoComentario);


        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _comentarioEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}