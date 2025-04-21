using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppViajesWirsolut.Models
{
    public enum EstadoViaje { Programado, Cancelado, Reprogramado, Finalizado, EnCurso}
    public enum CondicionClima { Inestable, CondicionesOptimas, SinInformacion}
    public class Viaje
    {
        [Required]
        public int ViajeId { get; set; }
        [Required]
        [MaxLength(150)]
        public string ViajeDestino { get; set; }
        [Required]
        public DateTime FechaDestino { get; set;}
        [Required]
        public EstadoViaje EstadoViaje {  get; set; }
        [Required]
        public DateTime FechaCreacion { get; set;}

        public CondicionClima CondicionClima { get; set;}

        // FK hacia Ciudad
        public int CiudadId { get; set; }
        [JsonIgnore]
        public Ciudad? Ciudad { get; set; }

        // FK hacia Vehiculo
        public int VehiculoId { get; set; }
        [JsonIgnore]
        public Vehiculo? Vehiculo { get; set; }

    }
}
