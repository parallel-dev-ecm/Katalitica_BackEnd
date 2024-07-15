namespace Katalitica_API.Models
{
    public class Colaboradores
    {
       
        
            public int clave { get; set; }
            public string nombre { get; set; }
            public string apellido_pat { get; set; }
            public string apellido_mat { get; set; }
            public DateTime fecha_nac { get; set; }
            public string estatus { get; set; }
            public DateTime fecha_ingreso { get; set; }
            public DateTime fecha_baja { get; set; }
            public string tipo_sanguineo { get; set; }
            public string tel_contacto { get; set; }
            public string email { get; set; }
            public string num_emergencia { get; set; }
            public string num_ss { get; set; }
            public string rfc { get; set; }
            public short id_categoria { get; set; }
            public short id_area { get; set; }
            public short id_puesto { get; set; }
            public string calle { get; set; }
            public string num_ext { get; set; }
            public string num_int { get; set; }
            public string cp { get; set; }
            public string colonia { get; set; }
            public string ciudad { get; set; }
            public string municipio { get; set; }
            public string estado { get; set; }
        }

    }

