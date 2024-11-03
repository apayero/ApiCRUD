using ApiCRUD.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCRUD.Controllers
{
    [Route("api/laptops")]
    public class LaptopController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LaptopController(ApplicationDbContext context)
        {
            this.context = context;
        }


        /// <summary>
        /// Este método lista todos los elementos de la tabla laptos
        /// Alex, puede hacer lo mismo para personas.
        /// 
        /// acciones a realizar:
        /// 1) Crear un Controller
        /// 2) hacer todo lo que estará en este archivo.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Laptop>> Get()
        {
            return await context.Laptops.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "ObtenerLaptopPorId")]
        public async Task<ActionResult<Laptop>> Get(int id)
        {
                             
            var laptop =  await context.Laptops.FirstOrDefaultAsync(x => x.Id == id);

            if (laptop is null)
            {
                return NotFound();
            }

            return laptop;

        }

        [HttpPost]
        public async Task<CreatedAtRouteResult> Post([FromBody]Laptop laptop)
        {
            context.Add(laptop);
            await context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerLaptopPorId", new { id = laptop.Id }, laptop);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,[FromBody] Laptop laptop)
        {
            var existeLaptop = await context.Laptops.AnyAsync(x => x.Id == id);

            if (!existeLaptop)
            {
                return NotFound();
            }

            laptop.Id = 12;
            context.Update(laptop);
            await context.SaveChangesAsync();
            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasBorradas = await context.Laptops.Where(x => x.Id == id).ExecuteDeleteAsync();

            if(filasBorradas == 0)
            {
                return NotFound();
            }

            return NoContent();

        }

    }
}
