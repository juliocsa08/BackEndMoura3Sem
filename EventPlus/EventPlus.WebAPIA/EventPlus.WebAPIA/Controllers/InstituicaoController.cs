using EventPlus.WebAPIA.DTO;
using EventPlus.WebAPIA.Interfaces;
using EventPlus.WebAPIA.Models;
using EventPlus.WebAPIA.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPIA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private IinstituiçãoRepository _instituicaoRepository;
    public InstituicaoController(IinstituiçãoRepository instituiçãoRepository)
    {
        _instituicaoRepository = instituiçãoRepository;
    }
    /// <summary>
    /// EndPoint da API que faz a chamada para o método de listar os tipos de evento
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de eventos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_instituicaoRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }


    }
    /// <summary>
    /// EndPoint Da API que faz a chamada para o método de buscar um tipo de evento especifico
    /// </summary>
    /// <param name="id">id do tipo de evento buscado</param>
    /// <returns>code 200 e o tipo de evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_instituicaoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }

    }
    /// <summary>
    /// EndPoint Da API que faz a chamada para o método de cadastrar um tipo de evento 
    /// </summary>
    /// <param name="institiucao">Tipo de evento a ser  cadastrado</param>
    /// <returns>code 201 e tipo de evento a ser cadastrado/returns>
    [HttpPost]
    public IActionResult Cadastrar(InstituicaoDTO institiucao)
    {
        
        try
        {

            
            var novaInstituicao = new Instituição
            {
                NomeFantasia = institiucao.NomeFantasia!,
                Cnpj = institiucao.Cnpj,
                Endereço = institiucao.Endereco!

            };
            _instituicaoRepository.Cadastrar(novaInstituicao);
            return StatusCode(201, novaInstituicao);
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }


    }
    /// <summary>
    /// EndPoint da API que faz a chamada para o método de atualizar um tipo de evento
    /// </summary>
    /// <param name="id">id do tipo evento a ser atualizado</param>
    /// <param name="tipoEvento">Tipo de evento com dados atualizados</param>
    /// <returns>Code 204 e o tipo de evento atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, InstituicaoDTO institiucao)
    {
        try
        {
            var instituiçãoAtualizado = new Instituição
            {
               NomeFantasia = institiucao.NomeFantasia!
            };
            _instituicaoRepository.Atualizar(id, instituiçãoAtualizado);

            return StatusCode(204, instituiçãoAtualizado);
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// EndPoint Da API que faz a chamada para o método de deletar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo do evento a ser excluido</param>
    /// <returns>Status Code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);
            return NoContent();

        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }


    }
}
