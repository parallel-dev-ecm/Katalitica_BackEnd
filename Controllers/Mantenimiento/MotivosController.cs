using Katalitica_API.Models.Mantenimiento;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers.Mantenimiento
{

    [ApiController]
    [Route("motivos")]
    public class MotivosController
    {
        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllMotivos");
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
        [Route("postMotivo")]
        public dynamic postCompany([FromBody] Motivos tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@cve_motivo", tractor.cve_motivo),
                new ParameterResource("@descripcion", tractor.descripcion),

            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoMotivos", parameters);
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
