using System;
using eagletechapi.dto.usuario;
using eagletechapi.entity.chamado.enums;
using eagletechapi.models.chamado;
using eagletechapi.models.chamado.enums;

namespace eagletechapi.dto.chamado
{
    public class ChamadoOut(Chamado chamado)
    {
        public long NumeroChamado { get; set; } = chamado.NumeroChamado;
        public string Titulo { get; set; } = chamado.Titulo;
        public string Descricao { get; set; } = chamado.Descricao;
        public Status Status { get; set; } = chamado.Status;
        public string? JustificativaFechamento {get;set;} = chamado.FechamentoJustificativa ?? "Não fechado";
        public Prioridade Prioridade { get; set; } = chamado.Prioridade;
        public Categoria Categoria { get; set; } = chamado.Categoria;
        public DateTime Abertura { get; set; } = chamado.Abertura;
        public DateTime Fechamento { get; set; } = chamado.Fechamento;
        public UserOut? Solicitante { get; set; } = chamado.Solicitante != null ? new UserOut(chamado.Solicitante) : null;
        public UserOut? Tecnico { get; set; } = chamado.Tecnico != null ? new UserOut(chamado.Tecnico) : null;
    }
}
