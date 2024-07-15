using Katalitica_API.Models;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers
{
    [ApiController]
    [Route("areas")]
    public class AreasGestionTController
    {
        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllAreas");
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
        [Route("postArea")]
        public dynamic postCompany([FromBody] AreasGestionT tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
               new ParameterResource("@clave", tractor.clave),
               new ParameterResource("@descripcion", tractor.descripcion),
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoAreas", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Area registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Area not registered",
                };
            }
        }
    }
}
