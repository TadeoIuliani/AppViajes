using AppViajesWirsolut.Context;
using AppViajesWirsolut.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppViajesWirsolut.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowLocalhost")]
    public class VehiculoController : Controller
    {
        private readonly AppDbContext _context;

        public VehiculoController(AppDbContext context)
        {
            _context = context;
        }


        // GET: VehiculoController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> Get(
            )
        {
            try
            { 
                var vehiculos = await _context.Vehiculos.ToListAsync();
                return Ok(vehiculos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        //Post
        [HttpPost]
        public async Task<ActionResult<Vehiculo>> Post(Vehiculo vehiculoNuevo)
        {
            try
            {
                _context.Vehiculos.Add(vehiculoNuevo);
                await _context.SaveChangesAsync(); //guardar cambios
                return CreatedAtAction(nameof(Get), new { id = vehiculoNuevo.VehiculoId}, vehiculoNuevo.Patente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Vehiculo>> Put(Vehiculo vehiculoActualizado, int id)
        {
            try
            {
                var vehiculo = await _context.Vehiculos.FindAsync(id);
                if (vehiculo == null) return NotFound();

                vehiculo.Patente = vehiculoActualizado.Patente;
                vehiculo.Tipo = vehiculoActualizado.Tipo;
                vehiculo.Marca = vehiculoActualizado.Marca;
                vehiculo.Viajes = vehiculoActualizado.Viajes;

                await _context.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception)
            {
                return BadRequest();

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehiculo>> Delete(int id)
        {
            try
            {
                var vehiculo = await _context.Vehiculos.FindAsync(id);
                if (vehiculo == null) return NotFound();

                _context.Vehiculos.Remove(vehiculo);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("probar-conexion")]
        public async Task<IActionResult> ProbarConexion()
        {
            try
            {
                var puedeConectar = await _context.Database.CanConnectAsync();
                if (puedeConectar)
                {
                    return Ok("✅ Conectado a la base de datos correctamente.");
                }
                else
                {
                    return StatusCode(500, "❌ No se pudo conectar a la base de datos.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al intentar conectar: {ex.Message}");
            }
        }

    }
}
