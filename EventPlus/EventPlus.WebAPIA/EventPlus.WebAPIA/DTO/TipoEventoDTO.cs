using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPIA.DTO;

public class TipoEventoDTO
{
    [Required(ErrorMessage = "O titulo do tipo de evento é obrigatório!")]
    public string? Titulo { get; set; }
}
