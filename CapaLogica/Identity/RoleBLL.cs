using Entidades.Models;
using Entidades.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using static Entidades.Models.RoleManagerDTO;
using System.Web.Http.Routing;

namespace CapaLogica.Identity
{
    public class RoleBLL : BaseBLL{
        public RoleBLL(HttpRequestMessage request) : base(request)
        {
        }

        public async Task<ResponseDTO<RoleDTO>> GetRole(string Id)
        {
            ResponseDTO<RoleDTO> response = new ResponseDTO<RoleDTO>();
            var role = await this.RoleManager.FindByIdAsync(Id);

            if (role != null)
            {
                response.Mensagge = "Role Encontrado";
                response.RowAffected = 1;
                response.Data = SetRoleDTO(role);
                return response;
            }
            response.Mensagge = "Role No Encontrado";
            response.Errors.Add(new ErrorMessage("401", "Error No Encontrado"));
            return response;
        }
    

        public ResponseDTO<List<RoleDTO>> GetAllRoles()
        {
            ResponseDTO<List<RoleDTO>> Response = new ResponseDTO<List<RoleDTO>>();
            List<RoleDTO> Roles = new List<RoleDTO>();
            List<IdentityRole> roles = RoleManager.Roles.ToList();
            foreach (IdentityRole role in roles) {
                Roles.Add(SetRoleDTO(role));
            }
            Response.Mensagge = "Listado De Roles";
            Response.Data = Roles;
            Response.RowAffected = roles.Count;
            return Response;
        }

        public async Task<ResponseDTO<RoleDTO>> Create(CreateRoleBindingModel model)
        {
            ResponseDTO<RoleDTO> response = new ResponseDTO<RoleDTO>(); 
            var role = new IdentityRole { Name = model.Name };
            response.Errors = this.VerifiedErrors(await this.RoleManager.CreateAsync(role));
            if (response.Errors.Count > 0)
            {
                response.Mensagge = "Role No Creado";
                return response;
            }
            response.Mensagge = "Role Ha Sido Creado";
            response.Data = SetRoleDTO(role);
            response.RowAffected = 1;
            return response;
        }

        public async Task<ResponseBasic> DeleteRole(string Id)
        {
            ResponseBasic response = new ResponseBasic();

            var role = await RoleManager.FindByIdAsync(Id);

            if (role != null)
            {
                response.Errors = VerifiedErrors(await this.RoleManager.DeleteAsync(role));
                if (response.Errors.Count > 0) {
                    response.Mensagge = "Role No Eliminado";
                }
                return response;
            }
            response.Mensagge = "Role Eliminado";
            response.RowAffected = 1;
            return response;
        }

        public async Task<ResponseBasic> ManageUsersInRole(UsersInRoleModel model)
        {
            ResponseBasic response = new ResponseBasic();
            var role = await this.RoleManager.FindByIdAsync(model.Id);
            int rows = 0;

            if (role == null)
            {
                response.Mensagge = "El Role No Existe";
                response.Errors.Add(new ErrorMessage("", "El Role No Ha Sido Encontrado"));
                return response;
            }

            foreach (string user in model.EnrolledUsers)
            {
                var appUser = await this.UserManager.FindByIdAsync(user);
                
                if (appUser == null)
                {
                    response.Errors.Add(new ErrorMessage("", String.Format("Usuario: {0} No Existe", user)));
                    continue;
                }

                if (!this.UserManager.IsInRole(user, role.Name))
                {
                    IdentityResult result = await UserManager.AddToRoleAsync(user, role.Name);

                    if (!result.Succeeded)
                    {
                        response.Errors.Add(new ErrorMessage("", String.Format("Usuario: {0} No Se Le Ha Agregado El Role", user)));
                    }
                    rows++;
                }
            }

            foreach (string user in model.RemovedUsers)
            {
                var appUser = await this.UserManager.FindByIdAsync(user);


                if (appUser == null)
                {
                    response.Errors.Add(new ErrorMessage("", String.Format("Usuario: {0} No Existe", user)));
                    continue;
                }


                IdentityResult result = await this.UserManager.RemoveFromRoleAsync(user, role.Name);

                if (!result.Succeeded)
                {
                    response.Errors.Add(new ErrorMessage("", String.Format("Usuario: {0} No Se Le Ha Agregado El Role", user)));
                }
                rows++;
            }

            response.RowAffected = rows;

            if (response.Errors.Count == 0){
                response.Mensagge = "Operación Realizada Con Exito";
                return response;
            }
            response.Mensagge = "Operacón Realizada Con Errores";
            return response;

        }

        private RoleDTO SetRoleDTO(IdentityRole role)
        {
            return new RoleDTO
            {
                Url = new UrlHelper(request).Link("GetRoleById", new { id = role.Id }),
                ID = role.Id,
                Name = role.Name
            };
        }
    }
}
