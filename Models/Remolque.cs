namespace Katalitica_API.Models
{
    public class Remolque
    {
        public string clave { get; set; }
        public string nombre_largo { get; set; }
        public string modelo { get; set; }
        public int año { get; set; }
        public string serie { get; set; }
        public string num_ejes { get; set; }
        public string placas { get; set; }
        public string capacidad_litros { get; set; }
        public string pq { get; set; }
        public int id_estatus { get; set; }
        public int id_producto { get; set; }
        public int id_centrocosto { get; set; }
        public int id_marcaet { get; set; }
    }
}
