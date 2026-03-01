using System;

namespace DtoModel.Casa
{
    public class CasaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Referencia { get; set; }
        public int? IdPropietarioPersona { get; set; }

        public int UserCreate { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
