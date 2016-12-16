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
        [HttpGet]
        [Authorize]
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Listado Con Los Usuarios Registrados")]
        public IHttpActionResult GetUsers()
        {
            return Ok(new AccountsBLL(Request).GetUsers());
        }

        [Authorize]
        [HttpGet]
        [Route("user/{id:guid}", Name = "GetUserById")]
        [SwaggerOperation("GetUserById")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Busca Un Usuario Por Id")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "El Usuario No Ha Sido Encontrado")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            ResponseDTO<UserDTO> user = await new AccountsBLL(Request).GetUser(Id);

            if (user.Data != null)
            {
                return Ok(user);
            }
            return Content<ResponseBasic>(HttpStatusCode.NotFound, user);
        }

        [Authorize]
        [HttpGet]
        [Route("user/{username}")]
        [SwaggerOperation("GetUserByUserName")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Busca Un Usuario Por Su Nombre De Usuario")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "El Usuario No Ha Sido Encontrado")]
        public async Task<IHttpActionResult> GetUserByName(string Username)
        {
            ResponseDTO<UserDTO> user = await new AccountsBLL(Request).GetUserByName(Username);

            if (user.Data != null)
            {
                return Ok(user);
            }

            return Content<ResponseBasic>(HttpStatusCode.NotFound, user);
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        [SwaggerOperation("CreateUser")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "El Usuario Ha Sido Creado")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "El Usuario No Ha Sido Creado")]
        public async Task<IHttpActionResult> CreateUser(CreateUserDTO createUserModel)
        {
            AccountsBLL accounts = new AccountsBLL(Request);
            ResponseDTO<UserDTO>  result = await accounts.CreateUser(createUserModel);
            if (result.RowAffected > 0)
            {
                string code = await accounts.GenerateCodeEmailConfirm(result.Data.Id);

                Uri linkConfirmEmail = new Uri(Url.Link("ConfirmEmailRoute", new { userId = result.Data.Id, code = code }));
                Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = result.Data.Id }));

                accounts.sendEmailConfirm(result.Data.Id, linkConfirmEmail );
                return Created(locationHeader, result);
            }
            return Content<ResponseBasic>(HttpStatusCode.BadRequest, result);
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation("ConfirmEmailRoute")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Error Al Activar La Cuenta.")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "La Cuenta Ha Sido Activada.")]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            ResponseBasic response = await new AccountsBLL(Request).confirmEmail(userId, code);
            if (response.Errors.Count > 0) {
                return Content<ResponseBasic>(HttpStatusCode.NotFound, response);
            }
            else {
                return Ok(response);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("ChangePassword")]
        [SwaggerOperation("ChangePassword")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "El Password Ha Sido Cambiado.")]
        public async Task<IHttpActionResult> ChangePassword(CambioPasswordDTO model)
        {
            return Ok(await new AccountsBLL(Request).ChangePassword(model));
        }

        [HttpDelete]
        [Route("user/{id:guid}")]
        [Authorize]
        [SwaggerOperation("DeleteUser")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "El Usuario Ha Sido Eliminado.")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            return Ok(await new AccountsBLL(Request).DeleteUser(id));
        }
    
    }
}