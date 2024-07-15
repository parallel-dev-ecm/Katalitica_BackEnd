using Katalitica_API.Models;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Katalitica_API.Controllers
{
    [ApiController]
    [Route("valvulasPresion")]
    public class ValvulasPresionController
    {
        [HttpGet]
        [Route("getAll")]
        public dynamic getAllMarcas()
        {
            DataSet marcasMotores = DBDatos.ListarTablas("sp_getAllValvulas");
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
        [Route("postValvula")]
        public dynamic postCompany([FromBody] ValvulasPresion tractor)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
               new ParameterResource("@clave", tractor.Clave),
            new ParameterResource("@nomo13_dlc", tractor.Nomo13Dlc),
            new ParameterResource("@nomo13_fec", tractor.Nomo13Fec),
            new ParameterResource("@nomo13_uv", tractor.Nomo13Uv),
            new ParameterResource("@nomo07_dlc", tractor.Nomo07Dlc),
            new ParameterResource("@nomo07_fec", tractor.Nomo07Fec),
            new ParameterResource("@nomo07_uv", tractor.Nomo07Uv),
            new ParameterResource("@fec_vertor", tractor.FecVertor),
            new ParameterResource("@uv_vertor", tractor.UvVertor),
            new ParameterResource("@fec_revvl", tractor.FecRevvl),
            new ParameterResource("@rev_valuv", tractor.RevValuv),
            new ParameterResource("@vef01_dia", tractor.Vef01Dia),
            new ParameterResource("@vef01_marca", tractor.Vef01Marca),
            new ParameterResource("@vef01_ffab", tractor.Vef01Ffab),
            new ParameterResource("@vef01_fins", tractor.Vef01Fins),
            new ParameterResource("@vef02_dia", tractor.Vef02Dia),
            new ParameterResource("@vef02_marca", tractor.Vef02Marca),
            new ParameterResource("@vef02_ffab", tractor.Vef02Ffab),
            new ParameterResource("@vef02_fins", tractor.Vef02Fins),
            new ParameterResource("@vef03_dia", tractor.Vef03Dia),
            new ParameterResource("@vef03_marca", tractor.Vef03Marca),
            new ParameterResource("@vef03_ffab", tractor.Vef03Ffab),
            new ParameterResource("@vef03_fins", tractor.Vef03Fins),
            new ParameterResource("@vseg01_dia", tractor.Vseg01Dia),
            new ParameterResource("@vseg01_marca", tractor.Vseg01Marca),
            new ParameterResource("@vseg01_serie", tractor.Vseg01Serie),
            new ParameterResource("@vseg01_ffab", tractor.Vseg01Ffab),
            new ParameterResource("@vseg01_fins", tractor.Vseg01Fins),
            new ParameterResource("@vseg02_dia", tractor.Vseg02Dia),
            new ParameterResource("@vseg02_marca", tractor.Vseg02Marca),
            new ParameterResource("@vseg02_serie", tractor.Vseg02Serie),
            new ParameterResource("@vseg02_ffab", tractor.Vseg02Ffab),
            new ParameterResource("@vseg02_fins", tractor.Vseg02Fins),
            new ParameterResource("@vnor01_dia", tractor.Vnor01Dia),
            new ParameterResource("@vnor01_marca", tractor.Vnor01Marca),
            new ParameterResource("@vnor01_ffab", tractor.Vnor01Ffab),
            new ParameterResource("@vnor01_fins", tractor.Vnor01Fins),
            new ParameterResource("@ins_part_fel", tractor.InsPartFel),
            new ParameterResource("@ins_part_reg", tractor.InsPartReg),
            new ParameterResource("@ins_part_inf", tractor.InsPartInf),
            new ParameterResource("@id_remolques", tractor.IdRemolques.ToString()),
            new ParameterResource("@id_marcavl", tractor.IdMarcavl.ToString())
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoValvulas", parameters);
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
