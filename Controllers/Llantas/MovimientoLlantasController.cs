using Katalitica_API.Models.Llantas;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers.Llantas
{
    [ApiController]
    [Route("movimientoLlantas")]
    public class MovimientoLlantasController {

        [HttpPost]
        [Route("postMovimientoLlanta")]
        public dynamic postLlantasCatalogo([FromBody] MovLlanta tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
    {
        new ParameterResource("@num_orden", tractor.num_orden),
        new ParameterResource("@fecha", tractor.fecha),
        new ParameterResource("@pos_montada", tractor.pos_montada),
        new ParameterResource("@num_montada", tractor.num_montada),
        new ParameterResource("@dot_montada", tractor.dot_montada),
        new ParameterResource("@mm_montada", tractor.mm_montada),
        new ParameterResource("@marca_montada", tractor.marca_montada),
        new ParameterResource("@piso_montada", tractor.piso_montada),
        new ParameterResource("@motivo_montada", tractor.motivo_montada),
        new ParameterResource("@destino_montada", tractor.destino_montada),
        new ParameterResource("@num_montadar", tractor.num_montadar),
        new ParameterResource("@dot_montadar", tractor.dot_montadar),
        new ParameterResource("@mm_montadar", tractor.mm_montadar),
        new ParameterResource("@marca_montadar", tractor.marca_montadar),
        new ParameterResource("@piso_montadar", tractor.piso_montadar),
        new ParameterResource("@id_estatus", tractor.id_estatus),
        new ParameterResource("@id_dolly", tractor.id_dolly),
        new ParameterResource("@id_remolque", tractor.id_remolque),
        new ParameterResource("@id_tractor", tractor.id_tractor),
        new ParameterResource("@id_motivo", tractor.id_motivo),
        new ParameterResource("@id_llanta", tractor.id_llanta)
    };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoMovimientosLlantas", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Llanta registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Llanta not registered",
                };
            }
        }


        [HttpGet]
    [Route("getAllMovimientoLlantas")]
    public dynamic getAllMarcas()
    {
        DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllMovimientosLlantas");
        Console.WriteLine(marcasMotores);
        string jsonRes = JsonConvert.SerializeObject(marcasMotores);

        return new
        {
            success = true,
            message = "Item found",
            result = jsonRes
        };
    }
    
    }
}
