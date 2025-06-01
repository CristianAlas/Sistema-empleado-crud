using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpeadoController : ControllerBase
    {
        private readonly BdcrudContext bdContext;

        public EmpeadoController(BdcrudContext _bdContext)
        {
            bdContext = _bdContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            var listaEmpleado = await bdContext.Empleados.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaEmpleado);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var empleado = await bdContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
            return StatusCode(StatusCodes.Status200OK, empleado);
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Empleado objeto)
        {
            await bdContext.Empleados.AddAsync(objeto);
            await bdContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok"} );
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Empleado objeto)
        {
            bdContext.Empleados.Update(objeto);
            await bdContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var empleado = await bdContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
            bdContext.Empleados.Remove(empleado);
            await bdContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }
    }
}
