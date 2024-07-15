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
    [Route("planesMantenimiento")]
    public class PlanManteniminetoController
    {

        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllUniquePlanesMantenimiento");
            Console.WriteLine(marcasMotores);
            string jsonRes = JsonConvert.SerializeObject(marcasMotores);

            return new
            {
                success = true,
                message = "Item found",
                result = jsonRes
            };
        } 


        [HttpGet]
        [Route("getAllPlanesDetalles")]
        public dynamic getAllMarcasDetalles()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllPlanesMantenimientoDetalles");
            Console.WriteLine(marcasMotores);
            string jsonRes = JsonConvert.SerializeObject(marcasMotores);
            return new
            {
                success = true,
                message = "Item found",
                result = jsonRes
            };
        }


        [HttpGet]
        [Route("getAllUniqueActividades")]
        public dynamic getAllUniqueActividades()
        {
            DataSet actividadesDataSet = DBDatos.ListarTablas("sp_getAllPlanesMantenimiento");
           // DataSet actividadesDataSet = DBDatos.ListarTablas("sp_getAllUniquePlanesMantenimiento");

            // Extract the DataTable from the DataSet
            DataTable actividadesTable = actividadesDataSet.Tables[0];

            // Use LINQ to get distinct values in the 'actividad' column
            var uniqueActividades = actividadesTable.AsEnumerable()
                .Select(row => row.Field<string>("descripcion"))
                .Distinct()
                .ToList();

            string jsonRes = JsonConvert.SerializeObject(uniqueActividades);

            return new
            {
                success = true,
                message = "Unique Actividades found",
                result = jsonRes
            };
        }

        [HttpPost]
        [Route("updateHoras")]
        public dynamic updateHoras(
          [FromBody] MecanicoHoras company)
        {
            List<ParameterResource> parameter = new List<ParameterResource>{
            new ParameterResource("@id",company.id.ToString()
            ),
            new ParameterResource("@horas", company.horas),

            };

            bool queryExecuted = DBDatos.Ejecutar("sp_updatePlanMantenimientoHoras", parameter);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Horas updated",
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
        [Route("updateMecanico")]
        public dynamic updateMecanico(
         [FromBody] MecanicoHoras company)
        {
            List<ParameterResource> parameter = new List<ParameterResource>{
            new ParameterResource("@id",company.id.ToString()
            ),
            new ParameterResource("@mecanico", company.mecanico),

            };

            bool queryExecuted = DBDatos.Ejecutar("sp_updatePlanMantenimientoMecanico", parameter);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Plan updated",
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
        [Route("postPlanMantenimiento")]
        public dynamic postCompany([FromBody] PlanMantenimiento tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@cve_plan", tractor.cve_plan),
                new ParameterResource("@descripcion", tractor.descripcion),
                new ParameterResource("@id_actividades", tractor.id_actividades.ToString()),
                new ParameterResource("@cve_actvplan", tractor.cve_actvplan),
                new ParameterResource("@kms_lim", tractor.kms_lim),
                new ParameterResource("@dias_lim", tractor.dias_lim),
                new ParameterResource("@tol_kms", tractor.tol_kms),
                new ParameterResource("@tol_dias", tractor.tol_dias),
                new ParameterResource("@tipo_et", tractor.tipo_et),


            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoPlanesMantenimiento", parameters);
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
        [Route("getByDescripcion")]
        public dynamic GetByActividad([FromBody] DescripcionRequest req)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@descripcion", req.Descripcion)



            };
            

            DataTable queryExecuted = DBDatos.Listar("sp_getPlanMantenimientoByDescripcion", parameters);
            string jsonRes = JsonConvert.SerializeObject(queryExecuted);
            return new
            {
                success = true,
                message = "Success",
                result = new
                {
                    company = jsonRes
                }
            };
        }

        [HttpPost]
        [Route("getByETAndKms")]
        public dynamic getByETAndKms([FromBody] PlanMantenimientoETAndKmRequest req)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@kms", req.Kms),
                


            };


            DataTable queryExecuted = DBDatos.Listar("sp_getPlanesMantenimientoByEtAndKms", parameters);
            string jsonRes = JsonConvert.SerializeObject(queryExecuted);
            return new
            {
                success = true,
                message = "Success",
                result = new
                {
                    company = jsonRes
                }
            };
        }

        [HttpPost]
        [Route("getByETAndKmsLessColumns")]
        public dynamic getByETAndKmsLessColumns([FromBody] PlanMantenimientoETAndKmRequest req)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@kms", req.Kms),
                new ParameterResource("@tipo_et", req.Tipo_et),




            };


            DataTable queryExecuted = DBDatos.Listar("sp_getPlanesMantenimientoByEtAndKmsLessColumns", parameters);
            string jsonRes = JsonConvert.SerializeObject(queryExecuted);
            return new
            {
                success = true,
                message = "Success",
                result = new
                {
                    company = jsonRes
                }
            };
        }
    }
}
