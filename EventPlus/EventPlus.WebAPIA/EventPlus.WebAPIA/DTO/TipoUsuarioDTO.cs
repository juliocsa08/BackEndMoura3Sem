using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.Controllers
{
    public class TipoUsuarioDTO


    {
        [Required(ErrorMessage = "O titulo do tipo de Usuario é obrigatório!")]
        public string? Titulo { get; set; }
    }
}