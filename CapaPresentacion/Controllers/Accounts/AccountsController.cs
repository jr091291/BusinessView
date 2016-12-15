using CapaLogica.Identity;
using Entidades.Models;
using Entidades.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace CapaPresentacion.Controllers.Accounts
{
    [RoutePrefix("apiv1/accounts")]
    public class AccountsController : BaseApiController
    {

        [Route("users")]
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Listado Con Los Usuarios Registrados")]
        public IHttpActionResult GetUsers()
        {
            return Ok(new AccountsBLL(Request).GetUsers());
        }

        [SwaggerOperation("GetUserById")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Busca Un Usuario Por Id")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "El Usuario No Ha Sido Encontrado")]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            ResponseDTO<UserDTO> user = await new AccountsBLL(Request).GetUser(Id);

            if (user.Data != null)
            {
                return Ok(user);
            }
            return Content<ResponseBasic>(HttpStatusCode.NotFound, user);
        }

        [SwaggerOperation("GetUserByUserName")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Busca Un Usuario Por Su Nombre De Usuario")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "El Usuario No Ha Sido Encontrado")]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string Username)
        {
            ResponseDTO<UserDTO> user = await new AccountsBLL(Request).GetUserByName(Username);

            if (user.Data != null)
            {
                return Ok(user);
            }

            return Content<ResponseBasic>(HttpStatusCode.NotFound, user);
        }

        [SwaggerOperation("CreateUser")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "El Usuario Ha Sido Creado")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "El Usuario No Ha Sido Creado")]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserDTO createUserModel)
        {
            ResponseDTO<UserDTO>  result = await new AccountsBLL(Request).CreateUser(createUserModel);
            if (result.RowAffected > 0)
            {
                Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = result.Data.Id }));
                return Created(locationHeader, result);
            }
            return Content<ResponseBasic>(HttpStatusCode.BadRequest, result);
        }
    }
}