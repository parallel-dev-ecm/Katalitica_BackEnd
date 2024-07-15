using Katalitica_API.Models.Mantenimiento;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers.Mantenimiento
{

    [ApiController]
    [Route("criterio")]
    public class CriteriosController
    {
        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllCriterios");
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
        [Route("postCriterio")]
        public dynamic postCompany([FromBody] Criterio tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@cve_ctr", tractor.cve_ctr),
                new ParameterResource("@nom_corto", tractor.nom_corto),
                new ParameterResource("@descripcion", tractor.descripcion),
                new ParameterResource("@prioridad", tractor.prioridad),

            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoCriterios", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Criterio registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Criterio not registered",
                };
            }
        }
    }
}
