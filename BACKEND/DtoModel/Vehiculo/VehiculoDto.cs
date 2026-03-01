namespace DtoModel.Vehiculo
{
    public class VehiculoDto
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public short Anio { get; set; }
        public string? Color { get; set; }
        public int IdPropietarioPersona { get; set; }
        public int? IdCasa { get; set; }
        public bool Estado { get; set; } = true;
        public int UserCreate { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
