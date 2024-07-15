namespace Katalitica_API.Models.Mantenimiento
{
    public class Actividades
    {
        public int? id { get; set; }

        public int id_criterio { get; set; }
        public int id_pieza { get; set; }
        public string descripcion { get; set; }
    }
}
