using ConnectPlus.DTO;
using ConnectPlus.Interface;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoContatoController : ControllerBase
{
    private ITipoContatoRepository _tipoContatoRepository;

    public TipoContatoController(ITipoContatoRepository tipoContatoRepository)
    {
        _tipoContatoRepository = tipoContatoRepository;
    }


    /// <summary>
    /// Endpoid da api que faz a chamada para o metodo de listar 
    /// </summary>
    /// <returns>eleretorna um statos code 200 e alista de tipo de eventos</returns>
    [HttpGet]
    public IActionResult List()
    {
        try
        {
            return Ok(_tipoContatoRepository.List());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// Endpoint da api que faz a chamada para o metodo de buscar por id
    /// </summary>
    /// <param name="id">id do tipo de evento buscado</param>
    /// <returns>status code 200 e o tipo de evento buscado</returns>

    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid Id)
    {
        try
        {
            return Ok(_tipoContatoRepository.BuscadoPorId(Id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);

        }
    }



    [HttpPost]

    public IActionResult Cadastrar(TipoContatoDTO tipoContato)
    {
        try
        {

            var novoTipoContato = new TipoContato
            {
                Titulo = tipoContato.Titulo!
            };

            _tipoContatoRepository.Cadastrar(novoTipoContato);
            return StatusCode(201, novoTipoContato);
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoit da api que faz a chamada de um metodo de atualizar um tipo de evento 
    /// </summary>
    /// <param name="id">Id do tipo evento a ser atualizado</param>
    /// <param name="tipoEvento">tipo de evento com dados</param>
    /// <returns>Status code 204 e o tipo de evento atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoContatoDTO tipoContato)
    {
        try
        {
            var TipoContatoAtualizado = new TipoContato
            {
                Titulo = tipoContato.Titulo!
            };


            _tipoContatoRepository.Atualizar(id, TipoContatoAtualizado);
            return StatusCode(204, tipoContato);
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// Endpoint da api que faz a chamada para o metodo de deletar um tipo de evento
    /// </summary>
    /// <param name="id">id do tipo do evento excluido</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _tipoContatoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
}
