namespace Katalitica_API.Models.Mantenimiento
{
    public class OrdenReparacion
    {
        public int? id { get; set; }
        public string? compania { get; set; }
        public string? taller { get; set; }
        public string? folio { get; set; }
        public string? motivo { get; set; }
        public string? estatus { get; set; }
        public DateTime? fech_entra { get; set; }
        public DateTime? fech_sal { get; set; }
        public string? operador { get; set; }
        public string? mecanico { get; set; }
        public string? tractor { get; set; }
        public string? remolque { get; set; }
        public string? dolly { get; set; }
        public string? observacion { get; set; }
        public int? id_actividades { get; set; }
        public int id_centrocosto { get; set; }
        public int id_taller { get; set; }
        public int id_operador { get; set; }
        public int? id_remolques { get; set; }
        public int? id_tractores { get; set; }
        public int? id_dollys { get; set; }
        public int id_compania { get; set; }
        public string? cve_act { get; set; }
        public string? descripcion { get; set; }
        public string? actividad { get; set; }
        public string? tiempo { get; set; }
        public string? chek { get; set; }
        public DateTime? fech_rep { get; set; }
        public int? km_remolques { get; set; }
        public int? km_editable_remolques { get; set; }
        public int? km_dollys { get; set; }
        public int? km_editable_dollys { get; set; }
        public int? km_tractores { get; set; }
        public int? km_editable_tractores { get; set; }
        public string? remolque2 { get; set; }
        public int? km_remolque2 { get; set; }
        public int? km_editable_remolque2 { get; set; }

    }
}
