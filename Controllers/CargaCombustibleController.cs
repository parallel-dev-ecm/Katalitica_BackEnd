using Katalitica_API.Models;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers
{

    [ApiController]
    [Route("cargasCombustible")]
    public class CargaCombustibleController
    {

        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllCargasCombustible");
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
        [Route("postCargaCombustibles")]
        public dynamic postCompany([FromBody] CargaCombustible tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@folio", tractor.folio),
                new ParameterResource("@serie", tractor.serie),
                new ParameterResource("@fecha_carga", tractor.fecha_carga.ToString("yyyy-MM-dd")),
                new ParameterResource("@fecha_captura",  tractor.fecha_captura.ToString("yyyy-MM-dd")),
                new ParameterResource("@id_combustible", tractor.id_combustible.ToString()),
                new ParameterResource("@id_estacionservicio", tractor.id_estacionservicio.ToString()),
                new ParameterResource("@litros", tractor.litros),
                new ParameterResource("@odometro", tractor.odometro),
                new ParameterResource("@clave_proveedor", tractor.clave_proveedor),
                new ParameterResource("@id_tractores", tractor.id_tractores.ToString()),
                new ParameterResource("@id_autoadmin", tractor.id_autoadmin.ToString()),
                new ParameterResource("@id_centrocostos", tractor.id_centrocostos.ToString()),
                new ParameterResource("@id_colaborador", tractor.id_colaborador.ToString()),
                new ParameterResource("@tipo_et", tractor.tipo_et)
            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoCargasCombustible", parameters);
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
