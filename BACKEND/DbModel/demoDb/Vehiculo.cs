using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.demoDb;

[Table("vehiculo")]
[Index("IdCasa", Name = "idx_vehiculo_casa")]
[Index("IdPropietarioPersona", Name = "idx_vehiculo_propietario")]
[Index("Placa", Name = "uq_vehiculo_placa", IsUnique = true)]
public partial class Vehiculo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("placa")]
    [StringLength(12)]
    public string Placa { get; set; } = null!;

    [Column("marca")]
    [StringLength(80)]
    public string Marca { get; set; } = null!;

    [Column("modelo")]
    [StringLength(80)]
    public string Modelo { get; set; } = null!;

    [Column("anio")]
    public short Anio { get; set; }

    [Column("color")]
    [StringLength(40)]
    public string? Color { get; set; }

    [Column("id_propietario_persona")]
    public int IdPropietarioPersona { get; set; }

    [Column("id_casa")]
    public int? IdCasa { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("user_create")]
    public int UserCreate { get; set; }

    [Column("user_update")]
    public int? UserUpdate { get; set; }

    [Column("date_created", TypeName = "timestamp")]
    public DateTime? DateCreated { get; set; }

    [Column("date_update", TypeName = "timestamp")]
    public DateTime? DateUpdate { get; set; }

    [ForeignKey("IdCasa")]
    public virtual Casa? Casa { get; set; }

    [ForeignKey("IdPropietarioPersona")]
    public virtual Persona Propietario { get; set; } = null!;
}
