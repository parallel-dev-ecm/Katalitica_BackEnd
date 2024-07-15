namespace Katalitica_API.Models
{
    public class VehiculoAdmin
    {
        public string clave { get; set; }
        public string nombre_largo { get; set; }
        public string modelo { get; set; }
        public int año { get; set; }
        public string serie_motor { get; set; }
        public string num_ejes { get; set; }
        public string placas { get; set; }
        public int id_estatus { get; set; }
        public int id_producto { get; set; }
        public int id_centrocosto { get; set; }
        public int id_marcaet { get; set; }
    }
}
