using Katalitica_API.Models;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
namespace Katalitica_API.Controllers
{
    [ApiController]
    [Route("tiposRemolques")]
    public class TiposRemolquesController
    {
        [HttpGet]
        [Route("getAllTipos")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllTiposRemolques");
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
        [Route("postTipo")]
        public dynamic postCompany([FromBody] TiposRemolques marcaMotor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", marcaMotor.clave),
                new ParameterResource("@descripcion", marcaMotor.descripcion),
                new ParameterResource("@num_ejes", marcaMotor.num_ejes.ToString())

            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoTiposRemolques", parameters);
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
