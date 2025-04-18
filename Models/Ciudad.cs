using System.ComponentModel.DataAnnotations;

namespace AppViajesWirsolut.Models
{
    public class Ciudad
    {
        [Required]
        public int CiudadId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
  
        public int IdApiCiudad { get; set; }
        // Relación: una ciudad puede tener muchos viajes 1:M
        public ICollection<Viaje> Viajes { get; set; }

        public Ciudad()
        {
            Viajes = new List<Viaje>();
        }


    }
}
