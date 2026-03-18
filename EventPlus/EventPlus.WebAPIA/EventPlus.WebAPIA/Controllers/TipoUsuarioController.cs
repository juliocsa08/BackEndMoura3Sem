using EventPlus.WebAPIA.DTO;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;
using EventPlus.WebAPIA.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

    public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoUsuarioRepository.Listar());
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            var tipoUsuario = _tipoUsuarioRepository.BuscarPorId(id);
            if (tipoUsuario == null)
                return NotFound("Tipo de usuário não encontrado.");

            return Ok(tipoUsuario);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpPost]
    public IActionResult Cadastrar(TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var novoTipoUsuario = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };

            _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);
            return StatusCode(201, novoTipoUsuario);
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }


    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var tipoUsuarioAtualizado = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };
            _tipoUsuarioRepository.Atualizar(id, tipoUsuarioAtualizado);
            return StatusCode(204, tipoUsuarioAtualizado);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoUsuarioRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}

