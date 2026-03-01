using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.demoDb;

[Table("casa")]
[Index("IdPropietarioPersona", Name = "idx_casa_propietario")]
public partial class Casa
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("direccion")]
    [StringLength(300)]
    public string? Direccion { get; set; }

    [Column("referencia")]
    [StringLength(200)]
    public string? Referencia { get; set; }

    [Column("id_propietario_persona")]
    public int? IdPropietarioPersona { get; set; }

    [Column("user_create")]
    public int UserCreate { get; set; }

    [Column("user_update")]
    public int? UserUpdate { get; set; }

    [Column("date_created", TypeName = "timestamp")]
    public DateTime? DateCreated { get; set; }

    [Column("date_update", TypeName = "timestamp")]
    public DateTime? DateUpdate { get; set; }

    [ForeignKey("IdPropietarioPersona")]
    public virtual Persona? Propietario { get; set; }
}
