using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Models
{
    public class CambioPasswordDTO
    {

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "IdUsuario")]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "AnteriorContraseña")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La Contraseña Debe Estar Entre 6 - 100 Caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NuevaContraseña")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmarContraseña")]
        [Compare("NuevaContraseña", ErrorMessage = "Las Contraseñas No Coinciden.")]
        public string ConfirmPassword { get; set; }

    }
}
