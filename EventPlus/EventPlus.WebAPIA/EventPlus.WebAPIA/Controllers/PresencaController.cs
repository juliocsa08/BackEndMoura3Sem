using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Repository;
using EventPlus.WebAPIA.DTO;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private IPresencaRepository _presencaRepository;

    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// Endpoint da API que lista as presenças de um usuário específico
    /// </summary>
    /// <param name="IdUsuario">Id do usuário a ser buscado</param>
    /// <returns>Lista de presenças do usuário</returns>
    [HttpGet("ListarMinhas/{IdUsuario}")]
    public IActionResult ListarMinhas(Guid IdUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(IdUsuario));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    /// <summary>
    /// Endpoint da API que faz a chamada para o método de lista de presenças
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_presencaRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Endpoint ad API que faz a chamada para o método de buscar uma presença
    /// </summary>
    /// <param name="id">id da presença buscada</param>
    /// <returns>Status code 200 e o evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de increver uma presenca
    /// </summary>
    /// <param name="presenca">presença a ser inscrita</param>
    /// <returns>Status code 201 e a presenca cadastrado</returns>
    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                Situacao = presenca.Situacao,
                IdEvento = presenca.IdEvento,
                IdUsuario = presenca.IdUsuario
            };

            _presencaRepository.Inscrever(novaPresenca);
            return StatusCode(201, novaPresenca);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de atualizar uma presença
    /// </summary>
    /// <param name="id">id da presença a ser atualizada</param>
    /// <returns>Status code 204 e a presença removida</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id)
    {
        try
        {
            _presencaRepository.Atualizar(id);
            return StatusCode(204, id);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    /// <summary>
    /// Endpoint da API que faz a chamada para o método de deletar uma presença
    /// </summary>
    /// <param name="id">Id da presença a ser deletada</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _presencaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
