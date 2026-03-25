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
    public int Id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string? FormaContato { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Imagem { get; set; }


    public Guid IdTipoContato { get; internal set; }
}
