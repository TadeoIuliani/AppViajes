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
    public class CiudadController : Controller
    {
        private readonly AppDbContext _context;

        public CiudadController(AppDbContext context)
        {
            _context = context;
        }
        // GET: CiudadController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ciudad>>> Get()
        {
            try
            {
                var ciudades = await _context.Ciudades.ToListAsync();
                return Ok(ciudades);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Ciudad>> Post(Ciudad ciudadNueva)
        {
            try
            {
                _context.Ciudades.Add(ciudadNueva);
                await _context.SaveChangesAsync(); //guardar cambios
                return CreatedAtAction(nameof(Get), new { id = ciudadNueva.CiudadId }, ciudadNueva.Nombre);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ciudad>> Put(Ciudad ciudadActualizada, int id)
        {
            try
            {
                var ciudad = await _context.Ciudades.FindAsync(id);
                if (ciudad == null) return NotFound();

                ciudad.Viajes = ciudadActualizada.Viajes;
                ciudad.Nombre = ciudadActualizada.Nombre;
                ciudad.CiudadId = ciudadActualizada.CiudadId;
                ciudad.IdApiCiudad = ciudadActualizada.IdApiCiudad;

                await _context.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception)
            {
                return BadRequest();

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Ciudad>> Delete(int id)
        {
            try
            {
                var ciudad = await _context.Ciudades.FindAsync(id);
                if (ciudad == null) return NotFound();

                _context.Ciudades.Remove(ciudad);
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
