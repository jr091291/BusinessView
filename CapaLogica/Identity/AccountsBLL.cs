using CapaDatos.Identity;
using CapaDatos.Models;
using Entidades.Models;
using Entidades.Response;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace CapaLogica.Identity
{
    public class AccountsBLL : BaseBLL
    {
        public AccountsBLL(HttpRequestMessage request) : base(request)
        {

        }

        public ResponseDTO<List<UserDTO>> GetUsers()
        {
            List<ApplicationUser> users = this.UserManager.Users.ToList();
            List<UserDTO> list = new List<UserDTO>();
            foreach (ApplicationUser user in users) {
                list.Add(this.setUserDTO(user));
            }
            return new ResponseDTO<List<UserDTO>>
            {
                Mensagge = "Listado De Usuarios",
                Data = list,
                RowAffected = list.Count
            };
        }

        public async Task<ResponseDTO<UserDTO>> GetUser(string Id)
        {
            ResponseDTO<UserDTO> response = new ResponseDTO<UserDTO>();
            var user = await this.UserManager.FindByIdAsync(Id);

            if (user != null) 
            {
                response.Mensagge = "Usuario Encontrado";
                response.Data = this.setUserDTO(user);
                response.RowAffected = 1;
                return response;
            }
            response.Mensagge = "El Usuario No Existe";
            return response;
        }

        public async Task<ResponseDTO<UserDTO>> GetUserByName(string username)
        {
            ResponseDTO<UserDTO> response = new ResponseDTO<UserDTO>();
            var user = await this.UserManager.FindByNameAsync(username);

            if (user != null)
            {
                response.Mensagge = "Usuario Encontrado";
                response.Data = this.setUserDTO(user);
                response.RowAffected = 1;
                return response;
            }
            response.Mensagge = "El Usuario No Existe";
            return response;
        }

        public async Task<ResponseDTO<UserDTO>> CreateUser(CreateUserDTO userDTO)
        {
            ResponseDTO<UserDTO> response = new ResponseDTO<UserDTO>();

            ApplicationUser user = new ApplicationUser
            {
                UserName = userDTO.Email,
                Email = userDTO.Email
            };

            response.Errors = this.VerifiedErrors(await this.UserManager.CreateAsync(user, userDTO.Password));

            if (response.Errors.Count > 0)
            {
                response.Mensagge = "El Usuario No Ha Sido Creado";
            }
            else
            {
                response.Mensagge = "Se Ha Creado Un Nuevo Usuario";
                response.Data = setUserDTO(user);
                response.RowAffected = 1;
            }
            return response;
        }

        private UserDTO setUserDTO(ApplicationUser user)
        {
            var roles = UserManager.GetRolesAsync(user.Id).Result;
            var claims = UserManager.GetClaimsAsync(user.Id).Result;
            var url = new UrlHelper(request).Link("GetUserById", new { id = user.Id });
            return new UserDTO
            {
                Url = url,
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles,
                Claims = claims
            };
        }
    }
}
