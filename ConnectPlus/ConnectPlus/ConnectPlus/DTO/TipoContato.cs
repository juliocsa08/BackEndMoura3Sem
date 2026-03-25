using System.ComponentModel.DataAnnotations;

namespace ConnectPlus.DTO;

public class TipoContatoDTO
{

    [Required(ErrorMessage = "O Tipo do contato obrigatorio")]
    public string? Titulo { get; set; }
}
