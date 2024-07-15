using Katalitica_API.Models.Mantenimiento;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers.Mantenimiento
{

    [ApiController]
    [Route("piezas")]
    public class PiezasController
    {

        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllPiezas");
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
        [Route("postPieza")]
        public dynamic postCompany([FromBody] Piezas tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@cve_pza", tractor.cve_pza),
                new ParameterResource("@nom_corto", tractor.nom_corto),
                new ParameterResource("@descripcion", tractor.descripcion),
         
            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoPiezas", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Pieza registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Pieza not registered",
                };
            }
        }

    }
}
