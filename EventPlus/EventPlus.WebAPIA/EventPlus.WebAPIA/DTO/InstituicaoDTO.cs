using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPIA.DTO;

public class InstituicaoDTO
{

    [Required(ErrorMessage = "O Endereço da Instituição é obrigatório!")]

    public string? Endereco { get; set; } = null!;

    [Required(ErrorMessage = "O NomeFantasia  da Instituição é obrigatório!")]
    public string? NomeFantasia { get; set; } = null!;

    [Required(ErrorMessage = "O CNPJ  da Instituição é obrigatório!")]
    public string? Cnpj { get; set; } = null!;
}

