using Katalitica_API.Models;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers
{
    [ApiController]
    [Route("marcasET")]
    public class MarcasETController : ControllerBase
    {
        [HttpGet]
        [Route("getAllMarcas")]
        public dynamic getAllMarcas()
        {
            DataSet marcasET = DBDatos.ListarTablas("GetMarcasET");
            Console.WriteLine(marcasET);
            string jsonRes = JsonConvert.SerializeObject(marcasET);

            return new
            {
                success = true,
                message = "Marcas found",
                result = jsonRes
            };
        }

        [HttpPost]
        [Route("postMarca")]
        public dynamic postCompany([FromBody] MarcaET marcaET)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", marcaET.clave),
                new ParameterResource("@descripcion", marcaET.descripcion),
                
            };

            bool queryExecuted = DBDatos.Ejecutar("insertIntoMarcasET", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Marca registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Marca not registered",
                };
            }


        }
    }
}
