using CapaLogica.Identity.Infraestructure;
using Entidades.Response;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace CapaLogica.Identity
{
    public class BaseBLL
    {
        protected ApplicationUserManager UserManager;
        protected HttpRequestMessage request;

        public BaseBLL(HttpRequestMessage request)
        {
            this.request = request;
            UserManager = ApplicationUserManager.getInstance(request);
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
