namespace Katalitica_API.Models.Mantenimiento
{
    public class Hubodometro
    {
        public string clave_et { get; set; }
        public string tipo_et { get; set; }
        public string estatus { get; set; }
        public decimal km_actuales { get; set; }
        public decimal km_totales { get; set; }
        public DateTime fec_ult_act { get; set; }
        public DateTime fec_instalacion { get; set; }
        public DateTime fec_baja { get; set; }
    }
}
