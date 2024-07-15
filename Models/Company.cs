namespace Katalitica_API.Models
{
	public class Company
	{
        public int? Id { get; set; }
        public string clave_compania { get; set; }
        public string rfc { get; set; }
        public string razon_social { get; set; }
        public string nombre_corto { get; set; }
        public string nombre_largo { get; set; }
        public string calle { get; set; }
        public string estado { get; set; }
        public string colonia { get; set; }
        public string codigo_postal { get; set; }
        public string telefono { get; set; }
        public string contacto_persona { get; set; }

    }
}

