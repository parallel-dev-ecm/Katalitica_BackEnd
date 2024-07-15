using Katalitica_API.Models;
using Katalitica_API.Models.Requests;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers
{
    [ApiController]
    [Route("colaboradores")]
    public class ColaboradoresController
    {
        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllColaboradores");
            Console.WriteLine(marcasMotores);
            string jsonRes = JsonConvert.SerializeObject(marcasMotores);

            return new
            {
                success = true,
                message = "Item found",
                result = jsonRes
            };
        }

        [HttpPost]
        [Route("postColaborador")]
        public dynamic postCompany([FromBody] Colaboradores tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", tractor.clave.ToString()),
                new ParameterResource("@nombre", tractor.nombre),
                new ParameterResource("@apellido_pat", tractor.apellido_pat),
                new ParameterResource("@apellido_mat", tractor.apellido_mat),
                new ParameterResource("@fecha_nac", tractor.fecha_nac.ToString("yyyy-MM-dd")),
                new ParameterResource("@estatus", tractor.estatus),
                new ParameterResource("@fecha_ingreso", tractor.fecha_ingreso.ToString("yyyy-MM-dd")),
                new ParameterResource("@fecha_baja", tractor.fecha_baja.ToString("yyyy-MM-dd")),
                new ParameterResource("@tipo_sanguinieo", tractor.tipo_sanguineo),
                new ParameterResource("@tel_contacto", tractor.tel_contacto),
                new ParameterResource("@email", tractor.email),
                new ParameterResource("@num_emergencia", tractor.num_emergencia),
                new ParameterResource("@num_ss", tractor.num_ss),
                new ParameterResource("@rfc", tractor.rfc),
                new ParameterResource("@id_categoria", tractor.id_categoria.ToString()),
                new ParameterResource("@id_area", tractor.id_area.ToString()),
                new ParameterResource("@id_puesto", tractor.id_puesto.ToString()),
                new ParameterResource("@calle", tractor.calle),
                new ParameterResource("@num_ext", tractor.num_ext),
                new ParameterResource("@num_int", tractor.num_int),
                new ParameterResource("@cp", tractor.cp),
                new ParameterResource("@colonia", tractor.colonia),
                new ParameterResource("@ciudad", tractor.ciudad),
                new ParameterResource("@municipio", tractor.municipio),
                new ParameterResource("@estado", tractor.estado)
            }; 


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoColaboradores", parameters);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Colaborador registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Colaborador not registered",
                };
            }
        }

        [HttpPost]
        [Route("getIdFromName")]
        public dynamic getColaboradorById([FromBody] CLaveRequest request)

        {
            List<ParameterResource> parameter = new List<ParameterResource>{
                new ParameterResource("@nombre", request.clave)
            };

            DataTable companies = DBDatos.Listar("sp_getColaboradorIdByName", parameter);
            Console.WriteLine(companies);
            string jsonCompany = JsonConvert.SerializeObject(companies);



            return new
            {
                success = true,
                message = "Success",
                result = new
                {
                    company = jsonCompany
                }
            };


        }
    }
}
