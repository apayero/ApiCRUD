using Microsoft.EntityFrameworkCore;

namespace ApiCRUD.Service
{
    public static class Paginacion
    {
        public static async Task<ColeccionDatos<T>> Paginar<T>(
            this IQueryable<T> query,
            int pagina,
            int cantidadPorPagina
        ) where T : class
        {
            var result = new ColeccionDatos<T>();

            result.Total = await query.CountAsync();
            result.Pagina = pagina;

            if (result.Total > 0)
            {
                result.Paginas = Convert.ToInt32(
                    Math.Ceiling(
                        Convert.ToDecimal(result.Total) / cantidadPorPagina
                    )
                );

                result.Registros = await query.Skip((pagina - 1) * cantidadPorPagina)
                                            .Take(cantidadPorPagina)
                                            .ToListAsync();
            }

            return result;
        }
    }
}
