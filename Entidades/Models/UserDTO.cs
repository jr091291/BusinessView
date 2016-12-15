using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Models
{
    public class UserDTO
    {
        public string Url { get; set; }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public IList<string> Roles { get; set; }

        public IList<Claim> Claims { get; set; }
    }
}
