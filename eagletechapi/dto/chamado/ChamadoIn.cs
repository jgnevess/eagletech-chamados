using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eagletechapi.entity.chamado.enums;
using eagletechapi.models.chamado;

namespace eagletechapi.dto.chamado
{
    public class ChamadoIn
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 40 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 500 caracteres")]

        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "A categoria é obrigatória")]
        public Categoria Categoria { get; set; } = Categoria.OUTROS;
        [Required(ErrorMessage = "A matrícula do solicitante é obrigatória")]
        public int UsuarioId { get; set; } = 0;
    }
}