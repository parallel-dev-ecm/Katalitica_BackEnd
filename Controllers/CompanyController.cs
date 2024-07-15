using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using Katalitica_API.Models;
using Katalitica_API.Models.Requests;
using Katalitica_API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Katalitica_API.Controllers
{

    [ApiController]
    [Route("company")]
    public class CompanyController : ControllerBase
    {


        [HttpGet]
        [Route("getAllCompanies")]
        public dynamic getAllCompanies()
        {
            DataSet companies = DBDatos.ListarTablas("sp_getAllFromGeneralesCompanias");
            Console.WriteLine(companies);
            string jsonCompany = JsonConvert.SerializeObject(companies);

            return new
            {
                success = true,
                message = "Company found",
                result = jsonCompany
            };
        }

        [HttpPost]
        [Route("postCompany")]
        public dynamic postCompany(
        string clave_compania,
        string rfc,
        string razon_social,
        string nombre_corto,
        string nombre_largo,
        string calle,
        string colonia,
        string estado,
        string codigo_postal,
        string contacto_persona,
        string telefono)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave_compania", clave_compania),
                new ParameterResource("@rfc", rfc),
                new ParameterResource("@razon_social", razon_social),
                new ParameterResource("@nombre_corto", nombre_corto),
                new ParameterResource("@nombre_largo", nombre_largo),
                new ParameterResource("@calle", calle),
                new ParameterResource("@colonia", colonia),
                new ParameterResource("@estado", estado),
                new ParameterResource("@codigo_postal", codigo_postal),
                new ParameterResource("@contacto_persona", contacto_persona),
                new ParameterResource("@telefono", telefono)
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoGeneralesCompanias", parameters);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Company registered",
                };
            }
            else {
                return new
                {
                    success = false,
                    message = "Company not registered",
                };
            }


        }
        [HttpGet]
        [Route("getAllUsers")]
        public dynamic getAllUsers()
        {
            DataSet res = DBDatos.ListarTablas("sp_getAllUsers");
            Console.WriteLine(res);
            string users = JsonConvert.SerializeObject(res);

            return new
            {
                success = true,
                message = "Users found",
                result = users
            };
        }

        [HttpGet]
        [Route("getAllFromCentroCostos")]
        public dynamic getAllCentroCostos()
        {
            DataSet centrosCostos = DBDatos.ListarTablas("sp_getAllFromCentroCostos");
            Console.WriteLine(centrosCostos);
            string jsonCentroCostos = JsonConvert.SerializeObject(centrosCostos);

            return new
            {
                success = true,
                message = "Centro de Costos found",
                result = jsonCentroCostos
            };
        }
        [HttpPost]
        [Route("postCentroCostos")]
        public dynamic postCentroCostos(
            [FromBody] CentroCostos centroCostos)
        {
            List<ParameterResource> parameters = new List<ParameterResource>
            {
                new ParameterResource("@clave", centroCostos.clave),
                new ParameterResource("@nombre", centroCostos.nombre),
                new ParameterResource("@calle", centroCostos.calle),
                new ParameterResource("@num_exterior", centroCostos.num_exterior),
                new ParameterResource("@num_interior", centroCostos.num_interior),
                new ParameterResource("@colonia", centroCostos.colonia),
                new ParameterResource("@codigo_postal", centroCostos.codigo_postal),
                new ParameterResource("@ciudad", centroCostos.ciudad),
                new ParameterResource("@estado", centroCostos.estado),
                new ParameterResource("@municipio", centroCostos.municipio),
                new ParameterResource("@telefono", centroCostos.telefono)
            };

            bool queryExecuted = DBDatos.Ejecutar("sp_insertIntoCentroCostos", parameters);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "CentroCostos registered",
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "CentroCostos not registered",
                };
            }


        }

        [HttpPost]
        [Route("updateCompany")]
        public dynamic updateCompany(
           [FromBody] Company company)
        {
            List<ParameterResource> parameter = new List<ParameterResource>{
            new ParameterResource("@Id",company.Id.ToString()
            ),
            new ParameterResource("@clave_compania", company.clave_compania),
            new ParameterResource("@rfc", company.rfc),
            new ParameterResource("@razon_social", company.razon_social),
            new ParameterResource("@nombre_corto",company.nombre_corto),
            new ParameterResource("@nombre_largo",company.nombre_largo),
            new ParameterResource("@calle",company.calle),
            new ParameterResource("@colonia",company.colonia),
            new ParameterResource("@estado",company.estado),
            new ParameterResource("@codigo_postal",company.codigo_postal),
            new ParameterResource("@contacto_persona",company.contacto_persona),
            new ParameterResource("@telefono",company.telefono),

            };
                    
            bool queryExecuted = DBDatos.Ejecutar("sp_UpdateFromGeneralesCompaniaById", parameter);
            Trace.WriteLine(queryExecuted);
            if (queryExecuted)
            {
                return new
                {
                    success = queryExecuted,
                    message = "Company updated",
                    result= 1
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
        [Route("getCompanyById")]
        public dynamic getCompanyById([FromBody] IdRequest idRequest)

        {
            List<ParameterResource> parameter = new  List<ParameterResource>{
                new ParameterResource("@Id", idRequest.Id)
            };

           DataTable companies = DBDatos.Listar("sp_getFromGeneralesCompaniasById", parameter);
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
        [Route("getUser")]
        public dynamic getUser([FromBody] LoginRequest loginRequest)
        {
            List<ParameterResource> parameter = new List<ParameterResource>{
            new ParameterResource("@username", loginRequest.username),
            new ParameterResource("@password", loginRequest.password)
            };
            Trace.WriteLine(loginRequest.username);
            Trace.WriteLine(loginRequest.password);
            Console.WriteLine("hello");

            bool result = DBDatos.ExecuteStoredProcedure("sp_UserExists", parameter);
            Console.WriteLine(result);

            return new
            {
                success = result,
                
            };


        }

        [HttpDelete]
        [Route("deleteCompanyById")]
        [Authorize]
        public dynamic deleteCompanyById(string id)
        {
            List<ParameterResource> parameter = new List<ParameterResource>{
                new ParameterResource("@Id", id)
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.ValidateToken(identity);
            

            if (!rToken.success)
            {
                return rToken;
            }

            UserModel _user = rToken.result;

            if(_user.userType != 1)
            {
                return new
                {
                    sucess = false,
                    message = "Cant delete company. Only admin users can.",
                };
            }

            bool queryExecuted = DBDatos.Ejecutar("sp_deleteFromGeneralesCompaniaById", parameter);

            return new
            {
                success = queryExecuted,
           
            };

        }


    }
}

