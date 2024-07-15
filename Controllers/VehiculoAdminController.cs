using Katalitica_API.Models;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
namespace Katalitica_API.Controllers
{
    [ApiController]
    [Route("vehiculosAdmin")]
    public class VehiculoAdminController
    {
        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllVehiculosAdmn");
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
        [Route("postVehiculo")]
        public dynamic postCompany([FromBody] VehiculoAdmin tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", tractor.clave),
                new ParameterResource("@nombre_largo", tractor.nombre_largo),
                new ParameterResource("@modelo", tractor.modelo),
                new ParameterResource("@año", tractor.año.ToString()),
                new ParameterResource("@serie_motor", tractor.serie_motor),
                new ParameterResource("@num_ejes", tractor.num_ejes),
                new ParameterResource("@placas", tractor.placas),
                new ParameterResource("@id_estatus", tractor.id_estatus.ToString()),
                new ParameterResource("@id_producto", tractor.id_producto.ToString()),
                new ParameterResource("@id_centrocosto", tractor.id_centrocosto.ToString()),
                new ParameterResource("@id_marcaet", tractor.id_marcaet.ToString()),


            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoVehiculosAdmin", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Vehiculo registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Vehiculo not registered",
                };
            }


        }
    }
}
