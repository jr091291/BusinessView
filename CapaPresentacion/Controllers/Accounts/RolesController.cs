using CapaLogica.Identity;
using Entidades.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static Entidades.Models.RoleManagerDTO;

namespace CapaPresentacion.Controllers.Accounts
{
    [RoutePrefix("apiv1/roles")]
    [Authorize(Roles = "Admin")]
    public class RolesController : BaseApiController
    {

        [Route("{id:guid}", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(string Id)
        {
            return Ok(await new RoleBLL(Request).GetRole(Id));
        }

        [Route("", Name = "GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            return Ok(new RoleBLL(Request).GetAllRoles());
        }

        [Route("create")]
        public async Task<IHttpActionResult> Create(Entidades.Models.RoleManagerDTO.CreateRoleBindingModel model)
        {
            ResponseDTO<RoleDTO> response = await new RoleBLL(Request).Create(model);
  
            Uri locationHeader = new Uri(Url.Link("GetRoleById", new { id = response.Data.ID }));
            return Created(locationHeader, response);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteRole(string Id)
        {
            return Ok(await new RoleBLL(Request).DeleteRole(Id));
        }

        [Route("ManageUsersInRole")]
        public async Task<IHttpActionResult> ManageUsersInRole(UsersInRoleModel model)
        {
            return Ok(await new RoleBLL(Request).ManageUsersInRole(model));
        }
    }
}