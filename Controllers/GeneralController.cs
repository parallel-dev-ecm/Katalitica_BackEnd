using Katalitica_API.Models;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Katalitica_API.Controllers
{

    [ApiController]
    [Route("general")]
    public class GeneralController
    {
        [HttpPost]
        [Route("updateTableDynamically")]
        public dynamic updateCompany(
           [FromBody] GeneralUpdateColumn company)
        {
            List<ParameterResource> parameter = new List<ParameterResource>{
            new ParameterResource("@id",company.Id.ToString()
            ),
            new ParameterResource("@tableName", company.TableName),
            new ParameterResource("@columnName", company.ColumnName),
            new ParameterResource("@value", company.Value),
    

            };

            bool queryExecuted = DBDatos.Ejecutar("sp_updateTableDynamically", parameter);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Table" + company.TableName + "updated",
                    result = 1
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Table not updated",
                };
            }


        }
    }
}
