using Katalitica_API.Models;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers
{

    [ApiController]
    [Route("marcasMotores")]
    public class MarcaMotoresController
    {
        [HttpGet]
        [Route("getAllMarcas")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllMarcasMotores");
            Console.WriteLine(marcasMotores);
            string jsonRes = JsonConvert.SerializeObject(marcasMotores);

            return new
            {
                success = true,
                message = "Marcas found",
                result = jsonRes
            };
        }

        [HttpPost]
        [Route("postMarca")]
        public dynamic postCompany([FromBody] MarcaMotor marcaMotor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", marcaMotor.clave),
                new ParameterResource("@descripcion", marcaMotor.descripcion),

            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertMarcaMotor", parameters);
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
