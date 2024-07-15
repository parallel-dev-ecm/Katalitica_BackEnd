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
    [Route("ordenReparacion")]
    public class OrdenReparacionController
    {
        [HttpGet]
        [Route("getAllActividades")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllOrdenReparacionActividades");
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
        [Route("getAllOrdenReparacion")]
        public dynamic getAllOrdenReparacion()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllOrdenReparacion");
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
        [Route("updateOrden")]
        public dynamic updateOrden(
        [FromBody] OrdenReparacion ordenReparacion)
        {
            List<ParameterResource> parameter = new List<ParameterResource>{
            new ParameterResource("@id",ordenReparacion.id.ToString()
            ),
             new ParameterResource("@compania", ordenReparacion.compania),
                new ParameterResource("@taller", ordenReparacion.taller),
                new ParameterResource("@folio", ordenReparacion.folio),
                new ParameterResource("@motivo", ordenReparacion.motivo),
                new ParameterResource("@estatus", ordenReparacion.estatus),
                new ParameterResource("@fech_entra", ordenReparacion.fech_entra),
                new ParameterResource("@fech_sal", ordenReparacion.fech_sal),
                new ParameterResource("@operador", ordenReparacion.operador),
                new ParameterResource("@mecanico", ordenReparacion.mecanico),
                new ParameterResource("@tractor", ordenReparacion.tractor),
                new ParameterResource("@remolque", ordenReparacion.remolque),
                new ParameterResource("@dolly", ordenReparacion.dolly),
                new ParameterResource("@observacion", ordenReparacion.observacion),
                new ParameterResource("@id_actividades", ordenReparacion.id_actividades),
                new ParameterResource("@id_centrocosto", ordenReparacion.id_centrocosto),
                new ParameterResource("@id_taller", ordenReparacion.id_taller),
                new ParameterResource("@id_operador", ordenReparacion.id_operador),
                new ParameterResource("@id_remolques", ordenReparacion.id_remolques),
                new ParameterResource("@id_tractores", ordenReparacion.id_tractores),
                new ParameterResource("@id_dollys", ordenReparacion.id_dollys),
                new ParameterResource("@id_compania", ordenReparacion.id_compania),
                new ParameterResource("@cve_act", ordenReparacion.cve_act),
                new ParameterResource("@descripcion", ordenReparacion.descripcion),
                new ParameterResource("@actividad", ordenReparacion.actividad),
                new ParameterResource("@tiempo", ordenReparacion.tiempo),
                new ParameterResource("@chek", ordenReparacion.chek),
                new ParameterResource("@fech_rep", ordenReparacion.fech_rep),
                new ParameterResource("@km_remolques", ordenReparacion.km_remolques),
                new ParameterResource("@km_editable_remolques", ordenReparacion.km_editable_remolques),
                new ParameterResource("@km_dollys", ordenReparacion.km_dollys),
                new ParameterResource("@km_editable_dollys", ordenReparacion.km_editable_dollys),
                new ParameterResource("@km_tractores", ordenReparacion.km_tractores),
                new ParameterResource("@km_editable_tractores", ordenReparacion.km_editable_tractores),
                new ParameterResource("@remolque2", ordenReparacion.remolque2),
                new ParameterResource("@km_remolque2", ordenReparacion.km_remolque2),
                new ParameterResource("@km_editable_remolque2", ordenReparacion.km_editable_remolque2),

            };

            bool queryExecuted = DBDatos.Ejecutar("sp_updateOrdenReparacion", parameter);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Orden updated",
                    result = 1
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "orden not registered",
                };
            }


        }



        [HttpPost]
        [Route("getOrdenById")]
        public dynamic getOrdenById([FromBody] IdRequest req)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@id", req.Id),
              
            };
            DataTable companies = DBDatos.Listar("sp_getOrdenReparacionById", parameters);
            Console.WriteLine(companies);
            string jsonRes = JsonConvert.SerializeObject(companies);
            return new
            {
                success = true,
                message = "Item found",
                result = jsonRes
            };
        }


        [HttpPost]
        [Route("getOrdenByFolio")]
        public dynamic getOrdenByFolio([FromBody] FolioRequest req)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@folio", req.Folio),

            };
            DataTable companies = DBDatos.Listar("sp_getOrdenReparacionByFolio", parameters);
            Console.WriteLine(companies);
            string jsonRes = JsonConvert.SerializeObject(companies);
            return new
            {
                success = true,
                message = "Item found",
                result = jsonRes
            };
        }


        [HttpGet]
        [Route("getIds")]
        public dynamic getIds()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getIdOrdenReparacionActividades");
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
        [Route("postActividades")]
        public dynamic postCompany([FromBody] OrdenReparaciónActividades hubodometro)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@compania", hubodometro.Compania),
                new ParameterResource("@folio", hubodometro.Folio),
                new ParameterResource("@cve_act", hubodometro.Cve_act),
                new ParameterResource("@pieza", hubodometro.Pieza),
                new ParameterResource("@descripcion", hubodometro.Descripcion),
                new ParameterResource("@identificador", hubodometro.Identificador),
                new ParameterResource("@mecanico", hubodometro.Mecanico),
                new ParameterResource("@tiempo", hubodometro.Tiempo),
                new ParameterResource("@chek", hubodometro.Chek),
                new ParameterResource("@fech_rep", hubodometro.Fecha_rep),
                new ParameterResource("@km_reparacion", hubodometro.Km_reparacion),
                new ParameterResource("@id_planesmantenimiento", hubodometro.Id_PlanesMantenimiento),
                new ParameterResource("@id_centrocosto", hubodometro.Id_CentroCosto),
                new ParameterResource("@id_taller", hubodometro.Id_Taller),
               // new ParameterResource("@id_operador", hubodometro.Id_Operador),
                new ParameterResource("@id_remolques", hubodometro.Id_Remolques),
                new ParameterResource("@id_tractores", hubodometro.Id_Tractores),
                new ParameterResource("@id_dollys", hubodometro.Id_Dollys),
                new ParameterResource("@id_compania", hubodometro.Id_Compania),
                new ParameterResource("@id_remolque2", hubodometro.Id_Remolque2),
                new ParameterResource("@id_OrdenReparacion", hubodometro.Id_OrdenReparacion),





            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoOrdenReparacionActividades", parameters);
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
        [Route("getActividadById")]
        public dynamic getActividadById([FromBody] IdRequest idRequest)

        {
            List<ParameterResource> parameter = new List<ParameterResource>{
                new ParameterResource("@id", idRequest.Id)
            };

            DataTable companies = DBDatos.Listar("sp_getOrdenReparacionActividadByID", parameter);
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
        [Route("getActividadByIdOrdenReparacion")]
        public dynamic getActividadByIdPlanMantenimiento([FromBody] IdRequest idRequest)

        {
            List<ParameterResource> parameter = new List<ParameterResource>{
                new ParameterResource("@id_ordenReparacion", idRequest.Id)
            };

            DataTable companies = DBDatos.Listar("sp_getOrdenReparacionActividadesByIdOrdenReparacion", parameter);
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
        [Route("postOrdenReparacion")]
        public dynamic postOrdenReparacion([FromBody] OrdenReparacion ordenReparacion)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@compania", ordenReparacion.compania),
                new ParameterResource("@taller", ordenReparacion.taller),
                new ParameterResource("@folio", ordenReparacion.folio),
                new ParameterResource("@motivo", ordenReparacion.motivo),
                new ParameterResource("@estatus", ordenReparacion.estatus),
                new ParameterResource("@fech_entra", ordenReparacion.fech_entra),
                new ParameterResource("@fech_sal", ordenReparacion.fech_sal),
                new ParameterResource("@operador", ordenReparacion.operador),
                new ParameterResource("@mecanico", ordenReparacion.mecanico),
                new ParameterResource("@tractor", ordenReparacion.tractor),
                new ParameterResource("@remolque", ordenReparacion.remolque),
                new ParameterResource("@dolly", ordenReparacion.dolly),
                new ParameterResource("@observacion", ordenReparacion.observacion),
                new ParameterResource("@id_actividades", ordenReparacion.id_actividades),
                new ParameterResource("@id_centrocosto", ordenReparacion.id_centrocosto),
                new ParameterResource("@id_taller", ordenReparacion.id_taller),
                new ParameterResource("@id_operador", ordenReparacion.id_operador),
                new ParameterResource("@id_remolques", ordenReparacion.id_remolques),
                new ParameterResource("@id_tractores", ordenReparacion.id_tractores),
                new ParameterResource("@id_dollys", ordenReparacion.id_dollys),
                new ParameterResource("@id_compania", ordenReparacion.id_compania),
                new ParameterResource("@cve_act", ordenReparacion.cve_act),
                new ParameterResource("@descripcion", ordenReparacion.descripcion),
                new ParameterResource("@actividad", ordenReparacion.actividad),
                new ParameterResource("@tiempo", ordenReparacion.tiempo),
                new ParameterResource("@chek", ordenReparacion.chek),
                new ParameterResource("@fech_rep", ordenReparacion.fech_rep),
                new ParameterResource("@km_remolques", ordenReparacion.km_remolques),
                new ParameterResource("@km_editable_remolques", ordenReparacion.km_editable_remolques),
                new ParameterResource("@km_dollys", ordenReparacion.km_dollys),
                new ParameterResource("@km_editable_dollys", ordenReparacion.km_editable_dollys),
                new ParameterResource("@km_tractores", ordenReparacion.km_tractores),
                new ParameterResource("@km_editable_tractores", ordenReparacion.km_editable_tractores),
                new ParameterResource("@remolque2", ordenReparacion.remolque2),
                new ParameterResource("@km_remolque2", ordenReparacion.km_remolque2),
                new ParameterResource("@km_editable_remolque2", ordenReparacion.km_editable_remolque2),



            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoOrdenReparacion", parameters);
            Trace.WriteLine(queryExecuted);
            return queryExecuted;
        }


        [HttpPost]
        [Route("getOrdenByAllColumns")]
        public dynamic getOrdenByAllColumns([FromBody] OrdenReparacion ordenReparacion)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@compania", ordenReparacion.compania),
                new ParameterResource("@taller", ordenReparacion.taller),
                new ParameterResource("@folio", ordenReparacion.folio),
                new ParameterResource("@motivo", ordenReparacion.motivo),
                new ParameterResource("@estatus", ordenReparacion.estatus),
                new ParameterResource("@fech_entra", ordenReparacion.fech_entra),
                new ParameterResource("@fech_sal", ordenReparacion.fech_sal),
                new ParameterResource("@operador", ordenReparacion.operador),
                new ParameterResource("@mecanico", ordenReparacion.mecanico),
                new ParameterResource("@tractor", ordenReparacion.tractor),
                new ParameterResource("@remolque", ordenReparacion.remolque),
                new ParameterResource("@dolly", ordenReparacion.dolly),
                new ParameterResource("@observacion", ordenReparacion.observacion),
                new ParameterResource("@id_actividades", ordenReparacion.id_actividades),
                new ParameterResource("@id_centrocosto", ordenReparacion.id_centrocosto),
                new ParameterResource("@id_taller", ordenReparacion.id_taller),
                new ParameterResource("@id_operador", ordenReparacion.id_operador),
                new ParameterResource("@id_remolques", ordenReparacion.id_remolques),
                new ParameterResource("@id_tractores", ordenReparacion.id_tractores),
                new ParameterResource("@id_dollys", ordenReparacion.id_dollys),
                new ParameterResource("@id_compania", ordenReparacion.id_compania),
                new ParameterResource("@cve_act", ordenReparacion.cve_act),
                new ParameterResource("@descripcion", ordenReparacion.descripcion),
                new ParameterResource("@actividad", ordenReparacion.actividad),
                new ParameterResource("@tiempo", ordenReparacion.tiempo),
                new ParameterResource("@chek", ordenReparacion.chek),
                new ParameterResource("@fech_rep", ordenReparacion.fech_rep),
                new ParameterResource("@km_remolques", ordenReparacion.km_remolques),
                new ParameterResource("@km_editable_remolques", ordenReparacion.km_editable_remolques),
                new ParameterResource("@km_dollys", ordenReparacion.km_dollys),
                new ParameterResource("@km_editable_dollys", ordenReparacion.km_editable_dollys),
                new ParameterResource("@km_tractores", ordenReparacion.km_tractores),
                new ParameterResource("@km_editable_tractores", ordenReparacion.km_editable_tractores),
                new ParameterResource("@remolque2", ordenReparacion.remolque2),
                new ParameterResource("@km_remolque2", ordenReparacion.km_remolque2),
                new ParameterResource("@km_editable_remolque2", ordenReparacion.km_editable_remolque2),
            };



            DataTable companies = DBDatos.Listar("sp_getOrdenMantenimientoAllColumns", parameters);
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
    }
}
