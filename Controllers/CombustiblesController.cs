using Katalitica_API.Models;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers
{
    [ApiController]
    [Route("combustibles")]
    public class CombustiblesController
    {
        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllCombustibles");
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
        [Route("postCombustible")]
        public dynamic postCompany([FromBody] Combustibles tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
               new ParameterResource("@clave", tractor.clave),
               new ParameterResource("@descripcion", tractor.descripcion),
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoCombustibles", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Combustible registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Combustible not registered",
                };
            }
        }
    }
}
