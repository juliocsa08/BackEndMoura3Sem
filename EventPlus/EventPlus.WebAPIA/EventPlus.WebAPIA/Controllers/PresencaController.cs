using EventPlus.WebAPIA.DTO;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPIA.Controllers;

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
    /// EndPoint da API que retorna uma lista de presenca de um usuário especifico
    /// </summary>
    /// <param name="IdUsuario">id do usuario para a filtragem</param>
    /// <returns>code 200 e uma lista de presença</returns>
    [HttpGet("ListarMinhas/{IdUsuario}")]
    public IActionResult BuscarMinhas(Guid IdUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(IdUsuario));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }

    }

    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                Situacao = presenca.Situacao,
                IdEvento = presenca.IdEvento,
                IdUsuario = presenca.IdUsuario,
            };
            _presencaRepository.Inscrever(novaPresenca);
            return StatusCode(201, novaPresenca);
        }
        catch (Exception e) 
        {

            return BadRequest(e.Message);
        }

    }
}
