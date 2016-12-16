using CapaDatos.Identity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica.Accounts.Infraestructure
{
    public class StartupOwingConfig
    {
        public static void RegisterOwinContextManager(IAppBuilder app) {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
        }
    }
}
