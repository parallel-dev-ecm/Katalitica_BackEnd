using Katalitica_API.Models.Mantenimiento;
using Katalitica_API.Models.Requests;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers.Mantenimiento
{
    [ApiController]
    [Route("actividades")]
    public class ActividadesController
    {
        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllActividades");
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
        [Route("getByDescripcion")]
        public dynamic getByDescripcion([FromBody] CLaveRequest req)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@descripcion", req.clave),

            };

            DataTable queryExecuted = DBDatos.Listar("GetIdByDescripcion", parameters);
            string jsonRes = JsonConvert.SerializeObject(queryExecuted);
            return new
            {
                success = true,
                message = "Success",
                result = new
                {
                    company = jsonRes
                }
            };
        }


        [HttpGet]
        [Route("getAllDescripciones")]
        public dynamic getAllDescripciones()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllDescriptionsActividades");
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
        [Route("postActividad")]
        public dynamic postCompany([FromBody] Actividades tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@id_criterio", tractor.id_criterio.ToString()),
                new ParameterResource("@id_pieza", tractor.id_pieza.ToString()),
                new ParameterResource("@descripcion", tractor.descripcion),

            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoActividades", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Actividad registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Actividad not registered",
                };
            }


           
        }
    }
}
