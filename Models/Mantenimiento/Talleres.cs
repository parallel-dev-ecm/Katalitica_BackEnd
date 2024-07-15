namespace Katalitica_API.Models.Mantenimiento
{
    public class Talleres
    {
        public int? Id { get; set; }
        public string cve_taller { get; set; }
        public string nom_corto { get; set; }
        public string descripcion { get; set; }
        public string compania { get; set; }
        public int id_centrocostos { get; set; }

    }
}
