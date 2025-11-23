using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eagletechapi.entity.usuario;
using System.ComponentModel.DataAnnotations;


namespace eagletechapi.dto.usuario
{
    public class UserIn
    {
        [Required]
        public string NomeCompleto { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        [Required]
        public string Telefone { get; set; } = string.Empty;
        [Required]
        public Funcao Funcao { get; set; } = Funcao.SOLICITANTE;
        [Required]
        public string Username { get; set; } = string.Empty;

    }
}