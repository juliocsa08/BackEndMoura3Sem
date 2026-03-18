using EventPlus.WebAPI.DTO;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace EventPlus.WebAPIA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;

    }
    /// <summary>
    /// EndPoint Da API que faz a chamada para o metodo de buscar um usuario por id 
    /// </summary>
    /// <param name="id">id do usuario a ser busacado</param>
    /// <returns>Status code 200 e o usuario buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamda para o método de cadastrar um usuario
    /// </summary>
    /// <param name="usuario">Usúario a ser cadastrado</param>
    /// <returns>Status Code 201 e o Usúario Cadastrado</returns>

    [HttpPost]

    public IActionResult Cadastrar(UsuarioDTO usuario)
    {
        try
        {
            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome!,
                Senha = usuario.Senha!,
                Email = usuario.Email!,
                IdTipoUsuario = usuario.IdTipoUsuario,

            };
            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201, novoUsuario);
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }

    }

}
