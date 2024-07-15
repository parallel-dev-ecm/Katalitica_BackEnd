namespace Katalitica_API.Models.Mantenimiento
{
    public class PlanMantenimiento
    {
        public string cve_plan { get; set; }
        public string descripcion { get; set; }
        public int id_actividades { get; set; }
        public string cve_actvplan { get; set; }
        public string kms_lim { get; set; }
        public string dias_lim { get; set; }
        public string tol_kms { get; set; }
        public string tol_dias { get; set; }
        public string tipo_et { get; set; }

    }
}
