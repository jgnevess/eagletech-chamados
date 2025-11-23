using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eagletechapi.entity.usuario;
using eagletechapi.models.usuario;

namespace eagletechapi.dto.usuario
{
    public class UserOut(Usuario usuario)
    {
        public int Matricula { get; set; } = usuario.Matricula;
        public string NomeCompleto { get; set; } = usuario.NomeCompleto;
        public string Telefone { get; set; } = usuario.Telefone;
        public Funcao Funcao { get; set; } = usuario.Funcao;
        public string Username { get; set; } = usuario.Username;
        public bool Ativo { get; set; } = usuario.Ativo;
        public Theme Theme { get; set; } = usuario.Theme;
    }
}