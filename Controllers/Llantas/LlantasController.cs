using Katalitica_API.Models.Llantas;
using Katalitica_API.Models.Mantenimiento;
using Katalitica_API.Models.Requests;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers.Llantas
{
    [ApiController]
    [Route("llantas")]
    public class LlantasController
    {
        [HttpGet]
        [Route("getAllMarcasLlantas")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllMarcasLlantas");
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
        [Route("getDescripcionesMarcasLlantas")]
        public dynamic getDescripcionesMarcasLlantas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getDescripcionesMarcasLlantas");
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
        [Route("getIdByDescripciones")]
        public dynamic getOrdenByFolio([FromBody] DescripcionRequest req)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@descripcion", req.Descripcion),

            };
            DataTable companies = DBDatos.Listar("sp_getMarcaLlantaByDescripcion", parameters);
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
        [Route("getAllModelos")]
        public dynamic getAllModulos()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllModelosLlantas");
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
        [Route("getAllLlantasEstatus")]
        public dynamic getAllLlantasEstatus()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllLlantasEstatus");
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
        [Route("getAllLlantasCatalogo")]
        public dynamic getAllLlantasCatalogo()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllLlantasLlantas");
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
        [Route("postLlantasCatalogo")]
        public dynamic postLlantasCatalogo([FromBody] LlantaCatalogo tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clavell", tractor.Clavell),
                new ParameterResource("@clave_et", tractor.Clave_et),
                new ParameterResource("@posicion", tractor.Posicion),
                new ParameterResource("@milimetros", tractor.Milimetros),
                new ParameterResource("@kms_ant", tractor.Kms_ant),
                new ParameterResource("@kms_act", tractor.Kms_act),
                new ParameterResource("@presion", tractor.Presion),
                new ParameterResource("@presion_est", tractor.Presion_est),
                new ParameterResource("@fecha_act", tractor.Fecha_act),
                new ParameterResource("@observaciones", tractor.Observaciones),
                new ParameterResource("@id_marcall", tractor.Id_marcall),
                new ParameterResource("@id_modeloll", tractor.Id_modeloll),
                new ParameterResource("@id_tipopiso", tractor.Id_tipopiso),
                new ParameterResource("@id_medidall", tractor.Id_medidall),
                new ParameterResource("@id_estatusll", tractor.Id_estatusll),
            };


            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoLlantasLlantas", parameters);
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


        [HttpPost]
        [Route("postEstatusLlantas")]
        public dynamic postEstatusLlantas([FromBody] Llanta tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", tractor.clave),
                new ParameterResource("@descripcion", tractor.descripcion),
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoLlantasEstatus", parameters);
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
        [Route("getAllTiposPisoLlantas")]
        public dynamic getAllTiposPisoLlantas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllTiposPisoLlantas");
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
        [Route("getAllMedidasLlantas")]
        public dynamic getAllMedidas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllMedidasLlantas");
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
        [Route("postMarcasLlantas")]
        public dynamic postMarca([FromBody] Llanta tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", tractor.clave),
                new ParameterResource("@descripcion", tractor.descripcion),
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoMarcasLlantas", parameters);
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

        [HttpPost]
        [Route("postModelosLlantas")]
        public dynamic postModelo([FromBody] Llanta tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", tractor.clave),
                new ParameterResource("@descripcion", tractor.descripcion),
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoModelosLlantas", parameters);
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
        [HttpPost]
        [Route("postTiposPisoLlantas")]
        public dynamic postTiposPiso([FromBody] Llanta tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", tractor.clave),
                new ParameterResource("@descripcion", tractor.descripcion),
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoTipoPiso", parameters);
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

        [HttpPost]
        [Route("postMedidasLlantas")]
        public dynamic postMedidas([FromBody] Llanta tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", tractor.clave),
                new ParameterResource("@descripcion", tractor.descripcion),
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoMedidasLlantas", parameters);
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

    }
}
