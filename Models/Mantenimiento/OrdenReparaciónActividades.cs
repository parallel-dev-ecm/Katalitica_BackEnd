namespace Katalitica_API.Models.Mantenimiento
{
    public class OrdenReparaciónActividades
    {
        public string? Compania { get; set; }
        public string? Folio { get; set; }
        public string? Cve_act { get; set; }
        public string? Pieza { get; set; }
        public string? Descripcion { get; set; }
        public string? Identificador { get; set; }
        public string? Mecanico { get; set; }
        public string? Tiempo { get; set; }
        public string? Chek  { get; set; }
        public DateTime? Fecha_rep { get; set; }
        public string? Km_reparacion { get; set; }
        public short? Id_PlanesMantenimiento { get; set; }
        public short? Id_CentroCosto { get; set; }
        public short? Id_Taller { get; set; }
      //  public short? Id_Operador { get; set; }
        public short? Id_Remolques { get; set; }
        public short? Id_Tractores { get; set; }
        public short? Id_Dollys { get; set; }
        public short? Id_Compania { get; set; }
        public short? Id_Remolque2 { get; set; }
        public short? Id_OrdenReparacion { get;set; }
    }
}
