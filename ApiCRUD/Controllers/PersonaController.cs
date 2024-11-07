using ApiCRUD.Entidades;
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
        public async Task<List<Persona>> Get()
        {
            return await context.Personas.ToListAsync();
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
    }
}
