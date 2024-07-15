using Katalitica_API.Models;
using Katalitica_API.Models.Mantenimiento;
using Katalitica_API.Models.Requests;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers.Mantenimiento
{
    [ApiController]
    [Route("hubodometro")]
    public class HubodometroController
    {
        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllHubodometros");
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
        [Route("postHubodometro")]
        public dynamic postCompany([FromBody] Hubodometro hubodometro)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave_et", hubodometro.clave_et),
                    new ParameterResource("@tipo_et", hubodometro.tipo_et),
                    new ParameterResource("@estatus", hubodometro.estatus),
                    new ParameterResource("@km_actuales", hubodometro.km_actuales.ToString()),
                    new ParameterResource("@km_totales", hubodometro.km_totales.ToString()),
                    new ParameterResource("@fec_ult_act", hubodometro.fec_ult_act),
                    new ParameterResource("@fec_instalacion", hubodometro.fec_instalacion),
                    new ParameterResource("@fec_baja", hubodometro.fec_baja),



            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoHubodometro", parameters);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Actividad registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Actividad not registered",
                };
            }
        }

        [HttpPost]
        [Route("getIdFromClave")]
        public dynamic getIdFromClave([FromBody] CLaveRequest request)

        {
            List<ParameterResource> parameter = new List<ParameterResource>{
                new ParameterResource("@clave", request.clave)
            };

            DataTable companies = DBDatos.Listar("sp_getHubodometroByClave", parameter);
            Console.WriteLine(companies);
            string jsonCompany = JsonConvert.SerializeObject(companies);



            return new
            {
                success = true,
                message = "Success",
                result = new
                {
                    company = jsonCompany
                }
            };


        }

        [HttpPost]
        [Route("updateKmActuales")]
        public dynamic updateKmActuales(
           [FromBody] UpdateKmRequest req)
        {
            List<ParameterResource> parameter = new List<ParameterResource>{
            new ParameterResource("@id", req.Id),
            new ParameterResource("@new_km_actuales", req.NewKm),


            };

            bool queryExecuted = DBDatos.Ejecutar("sp_updateHubodometroKmActuales", parameter);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Company updated",
                    result = 1
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Company not registered",
                };
            }


        }


        [HttpPost]
        [Route("updateKmTotales")]
        public dynamic updateKmTotales(
          [FromBody] UpdateKmRequest kmReq)
        {
            List<ParameterResource> parameter = new List<ParameterResource>{
            new ParameterResource("@id", kmReq.Id),
            new ParameterResource("@new_km_totales", kmReq.NewKm)
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_updateHubodometroKmTotales", parameter);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Hubodometro updated",
                    result = 1
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Hubodometro not updated",
                };
            }


        }


        [HttpPost]
        [Route("updateKmTotalesParameters")]
        public dynamic updateKmTotalesChaka(string _id, int new_km_totales)
        {
            List<ParameterResource> parameter = new List<ParameterResource>{
            new ParameterResource("@id", _id),
            new ParameterResource("@new_km_totales", new_km_totales)
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_updateHubodometroKmTotales", parameter);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Hubodometro updated",
                    result = 1
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Hubodometro not updated",
                };
            }


        }


    }
}
