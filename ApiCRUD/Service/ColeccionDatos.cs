namespace ApiCRUD.Service
{
    public class ColeccionDatos<T> where T : class
    {
        public bool TieneRegistros
        {
            get
            {
                return Registros != null && Registros.Any();
            }
        }
        public IEnumerable<T> Registros { get; set; } = null!;
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int Paginas { get; set; }
    }

}
