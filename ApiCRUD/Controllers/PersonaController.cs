using ApiCRUD.Entidades;
using ApiCRUD.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCRUD.Controllers
{
    [Route("api/persona")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PersonaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ColeccionDatos<Persona>> Get(int pagina, int cantidadPorPagina)
        {
            //   return await context.Personas.ToListAsync();
            var persona = await context.Personas.AsQueryable().Paginar(pagina, cantidadPorPagina);

            return persona;

        }

        [HttpGet ("{id:int}" ,Name ="ObtenerPersonaId")] 
        public async Task<ActionResult<Persona>> Get(int id)
        {
            var persona = await context.Personas.FirstOrDefaultAsync(x=>x.PersonaId==id);

            if (persona is null)
            {
                return NotFound("Registro no encontrado");
            }

            return persona;
        }


        [HttpPost]
        public async Task<CreatedAtRouteResult> Post([FromBody]Persona persona)
        {
            context.Add(persona);
            await context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerPersonaId" ,new {id=persona.PersonaId}, persona);
            
        }

        // Agregando código HttpPut -- Alex M.
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,[FromBody] Persona persona)
        {
            var existePersona = await context.Personas.AnyAsync(x => x.PersonaId == id);

            if (!existePersona)
            {
                return NotFound();
            }

            persona.PersonaId = 12;
            context.Update(persona);
            await context.SaveChangesAsync();
            return NoContent();
        }

    
        /// <summary>
        /// Fin código HttpPut
        /// Agregando código HttpDelete Alex M.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var filasBorradas = await context.Personas.Where(x => x.PersonaId == id)
                .ExecuteDeleteAsync();

            if(filasBorradas == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Fin Agregando código HttpDelete 
    }
}
