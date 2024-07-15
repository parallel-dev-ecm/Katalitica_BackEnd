namespace Katalitica_API.Models.Llantas
{
    public class LlantaCatalogo
    {
        public string? Clavell { get; set; }
        public string? Clave_et { get; set; }
        public string? Posicion { get; set; }
        public string? Milimetros { get; set; }
        public string? Kms_ant { get; set; }
        public string? Kms_act { get; set; }
        public string? Presion { get; set; }
        public string? Presion_est { get; set; }
        public DateTime? Fecha_act { get; set; }
        public string? Observaciones { get; set; }
        public short? Id_marcall { get; set; }
        public short? Id_modeloll { get; set; }
        public short? Id_tipopiso { get; set; }
        public short? Id_medidall { get; set; }
        public short? Id_estatusll { get; set; }
    }
}
