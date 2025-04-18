using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AppViajesWirsolut.Models
{
    public enum TipoVehiculo{Auto, Camion, Moto} //EN EL post enviar numerico
    public class Vehiculo
    {
        public Vehiculo()
        {
            Viajes = new List<Viaje>();
        }

        [Required]
        public int VehiculoId { get ; set ; }
        [Required]
        public TipoVehiculo Tipo { get ; set  ; }
        [Required]
        [MinLength(3)]
        public string Patente { get; set; }

        [Required]
        [MaxLength(50)]
        public string Marca { get; set; }

        // Relación: un vehículo puede estar en muchos viajes
        public ICollection<Viaje> Viajes { get; set; }
    }
}
