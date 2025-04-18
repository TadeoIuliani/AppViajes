using AppViajesWirsolut.Context;
using AppViajesWirsolut.Models;
using AppViajesWirsolut.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AppViajesWirsolut.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowLocalhost")]
    public class ViajeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IClimaService _climaService;

        public ViajeController(AppDbContext context, IClimaService climaService)
        {
            _context = context;
            _climaService = climaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Viaje>>> Get(
             [FromQuery] string? fechaDesde = null,
             [FromQuery] string? fechaHasta = null,
             [FromQuery] string? destino = null,
             [FromQuery] string? tipoVehiculo = null,
             [FromQuery] string? estadoViaje = null

            )
        {
            try
            {
                var query = _context.Viajes //Es para incluir objeto Ciudad && Vehiculo
                     .Include(v => v.Ciudad)
                    .Include(v => v.Vehiculo).
                    AsQueryable();

                // Filtro por fecha
                if ((!string.IsNullOrEmpty(fechaDesde) && !string.IsNullOrEmpty(fechaHasta)
                    && DateTime.TryParse(fechaDesde, out var fechaDesdeParsed) && DateTime.TryParse(fechaHasta, out var fechaHastaParsed)))
                {
                    query = query.Where(v => v.FechaDestino.Date >= fechaDesdeParsed.Date && v.FechaDestino.Date <= fechaHastaParsed.Date);
                }

                // Filtro por destino
                if (!string.IsNullOrEmpty(destino))
                {
                    query = query.Where(v => v.ViajeDestino.Contains(destino));
                }

                // Filtro por tipoVehiculo
                if (!string.IsNullOrEmpty(tipoVehiculo))
                {
                    query = query.Where(v => v.Vehiculo.Tipo.ToString().Contains(tipoVehiculo));
                }

                // Filtro por estadoViaje
                if (!string.IsNullOrEmpty(estadoViaje))
                {
                    if (Enum.TryParse<EstadoViaje>(estadoViaje, out var estadoEnum))
                    {
                        query = query.Where(v => v.EstadoViaje == estadoEnum);
                    }
                    else
                    {
                        return BadRequest("Estado de viaje inválido.");
                    }
                }


                var viajes = await query.ToListAsync();

                // Agrupar por ciudad

                //var ciudades = viajes.Select(v=>  v.Ciudad.IdApiCiudad).Distinct(); //Rompe por Vehiculo y Ciudad es null 400
                var ciudades = viajes
                    .Where(v => v.Ciudad != null)
                    .Select(v => v.Ciudad.IdApiCiudad)
                    .Distinct();


                var climaPorCiudad = new Dictionary<int, IEnumerable<object>>();

                foreach (var ciudad in ciudades)
                {
                    var clima = await _climaService.ObtenerClimaPorCiudadIdAsync(ciudad);
                    climaPorCiudad[ciudad] = clima;
                }

                // Asignar estado según clima
                foreach (var viaje in viajes)
                {
                    //un viaje con muchos climas
                    if(viaje.Ciudad != null && viaje.Vehiculo != null) //PROBAR IF
                    {
                        var climaPorCiudadDetalles = climaPorCiudad[viaje.Ciudad.IdApiCiudad];

                        //Comparo Fechas para saber si hay un clima para esa fecha
                        var climaDelDia = climaPorCiudadDetalles.FirstOrDefault( c => ((Clima)c).Fecha == viaje.FechaDestino);

                        //Asignacion de estado de clima
                        if(climaDelDia != null && ((Clima)climaDelDia).Estado == "Rain")
                        {
                            viaje.CondicionClima = CondicionClima.Inestable;
                        }
                        else if(climaDelDia != null)
                        {
                            viaje.CondicionClima = CondicionClima.CondicionesOptimas;
                        }
                        else
                        {
                            viaje.CondicionClima = CondicionClima.SinInformacion;
                        }
                    }
                }

                return Ok(viajes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Viaje>> Post(Viaje viajeNuevo)
        {
            // Validar el modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Si el modelo no es válido, retorna 400 con los errores de validación
            }

            if ((viajeNuevo.FechaDestino - viajeNuevo.FechaCreacion).TotalDays > 10 && viajeNuevo.FechaCreacion > DateTime.Now)
            {
                return BadRequest("Error: Fecha de destino");
            }
            try
            { 
                _context.Viajes.Add(viajeNuevo);
                await _context.SaveChangesAsync(); //guardar cambios
                return CreatedAtAction(nameof(Get), new { id = viajeNuevo.ViajeId }, viajeNuevo.Ciudad);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Viaje>> Put(Viaje viajeActualizado, int id)
        {
            try
            {
                var viaje = await _context.Viajes.FindAsync(id);
                if (viaje == null) return NotFound();

                viaje.ViajeDestino = viajeActualizado.ViajeDestino;
                viaje.EstadoViaje = viajeActualizado.EstadoViaje;
                viaje.Ciudad = viajeActualizado.Ciudad;
                viaje.ViajeId = viajeActualizado.ViajeId;
                viaje.Vehiculo = viajeActualizado.Vehiculo;
                viaje.CiudadId = viajeActualizado.CiudadId;
                viaje.VehiculoId = viajeActualizado.VehiculoId;
                viaje.FechaDestino = viajeActualizado.FechaDestino;
                viaje.FechaCreacion = viajeActualizado.FechaCreacion;


                await _context.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception)
            {
                return BadRequest();

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Viaje>> Delete(int id)
        {
            try
            {
                var viaje = await _context.Viajes.FindAsync(id);
                if (viaje == null) return NotFound();

                _context.Viajes.Remove(viaje);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}
