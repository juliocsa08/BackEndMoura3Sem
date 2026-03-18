using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPIA.DTO;

public class PresencaDTO
{
    
   public bool Situacao {  get; set; }
   public Guid IdUsuario { get; set; }
   public Guid IdEvento { get; set; }
}
