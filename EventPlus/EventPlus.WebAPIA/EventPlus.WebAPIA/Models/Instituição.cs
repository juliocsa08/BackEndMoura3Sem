using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventPlus.WebAPIA.Models;

[Table("Instituição")]
[Index("Cnpj", Name = "UQ__Institui__AA57D6B4A90C3F25", IsUnique = true)]
public partial class Instituição
{
    [Key]
    public Guid IdInstituicao { get; set; }

    [StringLength(100)]
    public string Endereço { get; set; } = null!;

    [StringLength(100)]
    public string NomeFantasia { get; set; } = null!;

    [Column("CNPJ")]
    [StringLength(14)]
    public string Cnpj { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("IdInstituicaoNavigation")]
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
