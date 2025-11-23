using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eagletechapi.entity.chamado.enums;
using eagletechapi.models.chamado.enums;

namespace eagletechapi.utils
{
    public class FiltrosRelatorio
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public Status? Status { get; set; }
        public Categoria? Categoria { get; set; }
        public Prioridade? Prioridade { get; set; }
        public TipoRelatorio TipoRelatorio { get; set; } = TipoRelatorio.Detalhado;
        public ArquivoRelatorio ArquivoRelatorio { get; set; } = ArquivoRelatorio.CSV_UTF8;
    }
}