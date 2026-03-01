using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.demoDb;

[Table("persona")]
[Index("IdTipoDocumento", Name = "fk_persona_tipo_documento")]
public partial class Persona
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_tipo_documento")]
    public int IdTipoDocumento { get; set; }

    [Column("numero_documento")]
    [StringLength(20)]
    public string? NumeroDocumento { get; set; }


    [Column("nombres")]
    [StringLength(100)]
    public string? Nombres { get; set; }

    [Column("apellido_paterno")]
    [StringLength(100)]
    public string? ApellidoPaterno { get; set; }

    [Column("apellido_materno")]
    [StringLength(100)]
    public string? ApellidoMaterno { get; set; }

    [Column("direccion")]
    [StringLength(300)]
    public string? Direccion { get; set; }

    [Column("telefono")]
    [StringLength(30)]
    public string? Telefono { get; set; }

    [Column("user_create")]
    public int UserCreate { get; set; }

    [Column("user_update")]
    public int? UserUpdate { get; set; }

    [Column("date_created", TypeName = "timestamp")]
    public DateTime? DateCreated { get; set; }

    [Column("date_update", TypeName = "timestamp")]
    public DateTime? DateUpdate { get; set; }

    [ForeignKey("IdTipoDocumento")]
    [InverseProperty("Persona")]
    public virtual PersonaTipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;
}
