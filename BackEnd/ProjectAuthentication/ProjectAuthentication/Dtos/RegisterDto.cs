using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAuthentication.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email deve ter no mínimo 3 caracteres")]
        public string FullName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email deve ter no mínimo 3 caracteres")]
        public string Email { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "Sua senha deve ter entre 8 a 20 caracteres")]
        public string Password { get; set; }
    }
}
