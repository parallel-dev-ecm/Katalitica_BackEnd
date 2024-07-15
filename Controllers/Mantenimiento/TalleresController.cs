using Katalitica_API.Models.Mantenimiento;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers.Mantenimiento
{
    [ApiController]
    [Route("talleres")]
    public class TalleresController
    {

        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllTalleres");
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
        [Route("postTaller")]
        public dynamic postCompany([FromBody] Talleres tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@cve_taller", tractor.cve_taller),
                new ParameterResource("@nom_corto", tractor.nom_corto),
                new ParameterResource("@descripcion", tractor.descripcion),
                new ParameterResource("@compania", tractor.compania),
                new ParameterResource("@id_centrocostos", tractor.id_centrocostos.ToString())


            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoTalleres", parameters);
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
