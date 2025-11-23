using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eagletechapi.dto.usuario
{
    public class PasswordUpdate(int matricula, string oldPassword, string newPassword, string confirmNewPassword)
    {
        public int Matricula { get; set; } = matricula;
        public string OldPassword { get; set; } = oldPassword;

        [StringLength(40, MinimumLength = 12, ErrorMessage = "A senha deve ter entre 12 e 40 caracteres")]
        [
            RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z0-9]).{12,}$",
            ErrorMessage = "A senha deve conter pelo menos 12 caracteres, incluindo uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")
        ]
        public string NewPassword { get; set; } = newPassword;
        public string ConfirmNewPassword { get; set; } = confirmNewPassword;
    }
}