using Katalitica_API.Models;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers
{
    [ApiController]
    [Route("estacionServicio")]
    public class EstacionServicioController
    {
        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllFromEstacionServicio");
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
        [Route("postEstacionServicio")]
        public dynamic postCompany([FromBody] EstacionServicio tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
               new ParameterResource("@clave", tractor.clave),
               new ParameterResource("@nombre", tractor.nombre),
               new ParameterResource("@num_est", tractor.num_est),
               new ParameterResource("@ubicacion", tractor.ubicacion),
               new ParameterResource("@clave_proveedor", tractor.clave_proveedor)

            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoEstacionServicio", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Estacion registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Estacion not registered",
                };
            }
        }

    }
}
