namespace Katalitica_API.Models
{
    public class CargaCombustible
    {
        public string folio { get; set; }
        public string serie { get; set; }
        public DateTime fecha_carga { get; set; }
        public DateTime fecha_captura { get; set; }
        public int id_combustible { get; set; }
        public int id_estacionservicio { get; set; }
        public string litros { get; set; }
        public string odometro { get; set; }
        public string clave_proveedor { get; set; }
        public int id_tractores { get; set; }
        public int id_autoadmin { get; set; }
        public int id_centrocostos { get; set; }
        public int id_colaborador { get; set; }
        public string tipo_et { get; set; }
    }
}
