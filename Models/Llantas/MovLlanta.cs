namespace Katalitica_API.Models.Llantas
{
    public class MovLlanta

    {
        public string num_orden { get; set; }
        public DateTime fecha { get; set; }
        public string pos_montada { get; set; }
        public string num_montada { get; set; }
        public string dot_montada { get; set; }
        public string mm_montada { get; set; }
        public string marca_montada { get; set; }
        public string piso_montada { get; set; }
        public string motivo_montada { get; set; }
        public string destino_montada { get; set; }
        public string num_montadar { get; set; }
        public string dot_montadar { get; set; }
        public string mm_montadar { get; set; }
        public string marca_montadar { get; set; }
        public string piso_montadar { get; set; }
        public short id_estatus { get; set; }
        public short id_dolly { get; set; }
        public short id_remolque { get; set; }
        public short id_tractor { get; set; }
        public short? id_motivo { get; set; }
        public short id_llanta { get; set; }
    }
}
