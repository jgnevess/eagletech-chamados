using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eagletechapi.dto.chamado;
using eagletechapi.entity.chamado.enums;
using eagletechapi.models.chamado.enums;
using eagletechapi.models.usuario;

namespace eagletechapi.models.chamado
{
    public class Chamado
    {
        [Key]
        public long NumeroChamado { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string? FechamentoJustificativa {get; set;}
        public Status Status { get; set; }
        public Prioridade Prioridade { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime Abertura { get; set; }
        public DateTime Fechamento { get; set; }
        public Usuario? Solicitante { get; set; }
        public Usuario? Tecnico { get; set; }

        public Chamado()
        {
            this.NumeroChamado = 0L;
            this.Titulo = string.Empty;
            this.Descricao = string.Empty;
            this.Status = Status.ABERTO;
            this.Prioridade = Prioridade.CRITICA;
            this.Categoria = Categoria.OUTROS;
            this.Abertura = DateTime.Now;
            this.Fechamento = DateTime.Now;
            this.Solicitante = null;
            this.Tecnico = null;
        }

        public Chamado(ChamadoIn chamadoIn, Usuario solicitante)
        {
            this.NumeroChamado = 0L;
            this.Titulo = chamadoIn.Titulo;
            this.Descricao = chamadoIn.Descricao;
            this.Status = Status.ABERTO;
            this.Prioridade = Prioridade.BAIXA;
            this.Categoria = chamadoIn.Categoria;
            this.Abertura = DateTime.Now;
            this.Fechamento = DateTime.MinValue;
            this.Solicitante = solicitante;
            this.Tecnico = null;
        }

        public void AceitarChamado(Usuario tecnico)
        {
            this.Status = Status.EM_ANDAMENTO;
            this.Tecnico = tecnico;
        }

        public void FecharChamado(string fechamentoJustificativa)
        {
            this.Fechamento = DateTime.Now;
            this.Status = Status.FECHADO;
            this.FechamentoJustificativa = fechamentoJustificativa;
        }

        public void ReabrirChamado()
        {
            this.Status = Status.ABERTO;
            this.Fechamento = DateTime.MinValue;
            this.Tecnico = null;
            this.FechamentoJustificativa = null;
        }


    }
}