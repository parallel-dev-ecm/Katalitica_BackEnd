using Katalitica_API.Models.Llantas;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers.Llantas
{

    [ApiController]
    [Route("motivosLlantas")]
    public class MotivosLlantasController
    {
        [HttpGet]
        [Route("getAllMotivosLlantas")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("Sp_getAllMotivosLlantas");
            Console.WriteLine(marcasMotores);
            string jsonRes = JsonConvert.SerializeObject(marcasMotores);

            return new
            {
                success = true,
                message = "Llantas found",
                result = jsonRes
            };
        }

        [HttpPost]
        [Route("postMovimientoLlanta")]
        public dynamic postLlantasCatalogo([FromBody] MotivosLlantas tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
    {
        new ParameterResource("@descripciom", tractor.descripciom),
        new ParameterResource("@Clave", tractor.Clave),
        
    };


            bool queryExecuted = DBDatos.Ejecutar("Sp_insertIntoMotivosLlanta", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Motivo registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Motivo not registered",
                };
            }
        }

    }
}

