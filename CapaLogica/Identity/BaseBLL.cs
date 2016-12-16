using CapaLogica.Accounts.Infraestructure;
using CapaLogica.Identity.Infraestructure;
using Entidades.Response;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Net.Http;

namespace CapaLogica.Identity
{
    public class BaseBLL
    {
        protected ApplicationUserManager UserManager;
        protected ApplicationRoleManager RoleManager;
        protected HttpRequestMessage request;

        public BaseBLL(HttpRequestMessage request)
        {
            this.request = request;
            UserManager = ApplicationUserManager.getInstance(request);
            RoleManager = ApplicationRoleManager.getInstance(request);
        }

        public List<ErrorMessage> VerifiedErrors(IdentityResult result) {
            List<ErrorMessage> response = new List<ErrorMessage>();

            if (result == null)
            {
                response.Add(new ErrorMessage("500", "Error En El Servidor"));
            }
            else if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        response.Add(new ErrorMessage("", error));
                    }
                }
            }
            return response;
        }
    }
}
