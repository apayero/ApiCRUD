using ApiCRUD.Entidades;
using ApiCRUD.Migrations;
using ApiCRUD.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiCRUD.Controllers
{
    [Route("api/prueba")]
    public class PruebaController : Controller
    {
        private readonly ApplicationDbContext context;

        public PruebaController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<ColeccionDatos<Laptop>>> GetAll(int pagina, int cantidadPorPagina)
        {
            var laptops = await context.Laptops.AsQueryable().Paginar(pagina, cantidadPorPagina);


            return laptops;
        }


    }
}
