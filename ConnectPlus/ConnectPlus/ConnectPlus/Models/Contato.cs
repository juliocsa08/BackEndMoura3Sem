using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus.Models;

[Table("Contato")]
public partial class Contato
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string? FormaContato { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Imagem { get; set; }

    public Guid? TipoContatoId { get; set; }

    [ForeignKey("TipoContatoId")]
    [InverseProperty("Contatos")]
    public virtual TipoContato? TipoContato { get; set; }
}
