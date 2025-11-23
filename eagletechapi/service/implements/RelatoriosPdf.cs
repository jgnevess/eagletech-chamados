using eagletechapi.models.chamado;
using eagletechapi.utils;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using eagletechapi.models.chamado.enums;

public class RelatorioPdf(List<Chamado> chamados, FiltrosRelatorio filtrosRelatorio) : IDocument
{
    private readonly List<Chamado> _chamados = chamados;
    private readonly FiltrosRelatorio _filtrosRelatorio = filtrosRelatorio;
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    [Obsolete]
    public void Compose(IDocumentContainer container)
    {
        // Gera o conteúdo do PDF com base na lista de chamados e nos filtros fornecidos

        var abertos = _chamados.Count(c => c.Status.Equals(Status.ABERTO));
        var emAndamento = _chamados.Count(c => c.Status.Equals(Status.EM_ANDAMENTO));
        var fechados = _chamados.Count(c => c.Status.Equals(Status.FECHADO));

        container.Page(page =>
        {
            page.Margin(40);
            page.Size(PageSizes.A4);
            page.DefaultTextStyle(x => x.FontSize(10));

            page.Header()
                .Text($"Relatório de Chamados - {_filtrosRelatorio.TipoRelatorio}")
                .FontSize(18).Bold().AlignCenter();

            page.Content().Column(col =>
            {
                col.Item().PaddingVertical(10)
                    .Text($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm} - Período: {_filtrosRelatorio.DataInicio:dd/MM/yyyy} a {_filtrosRelatorio.DataFim:dd/MM/yyyy}");

                col.Item().Text("Resumo").Bold();
                col.Item().Row(row =>
                {
                    row.RelativeColumn().Text($"Total: {_chamados.Count}").Bold();
                    row.RelativeColumn().Text($"Abertos: {abertos}").Bold();
                    row.RelativeColumn().Text($"Em andamento: {emAndamento}").Bold();
                    row.RelativeColumn().Text($"Fechados: {fechados}").Bold();
                });

                col.Item().PaddingBottom(15);

                foreach (var c in _chamados)
                {
                    col.Item().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten3)
                        .PaddingVertical(8)
                        .Column(chCol =>
                        {
                            chCol.Item().Row(row =>
                            {
                                row.RelativeColumn().Text($"Número do chamado: {c.NumeroChamado}").Bold().FontSize(15);
                            });

                            chCol.Item().Row(row =>
                            {
                                row.RelativeColumn().Text($"Status: {c.Status.ToString()}");
                                row.RelativeColumn().Text($"Prioridade: {c.Prioridade.ToString()}");
                                row.RelativeColumn().Text($"Categoria: {c.Categoria.ToString()}");
                            });

                            if (_filtrosRelatorio.TipoRelatorio.Equals(TipoRelatorio.Detalhado))
                            {
                                chCol.Item().Row(row =>
                                {
                                    row.RelativeColumn().Text($"Solicitante: {c.Solicitante!.NomeCompleto}");
                                    row.RelativeColumn().Text($"Técnico: {(c.Tecnico != null ? c.Tecnico.NomeCompleto : "Sem técnico")}");
                                });

                                chCol.Item().Row(row =>
                                {
                                    row.RelativeColumn().Text($"Abertura: {c.Abertura.ToString("dd/MM/yyyy HH:mm")}");
                                    row.RelativeColumn().Text($"Fechamento: {(c.Fechamento.Year == 1 ? "Em Aberto" : c.Fechamento.ToString("dd/MM/yyyy HH:mm"))}");
                                });

                                chCol.Item().PaddingTop(4)
                                    .Text($"Descrição: {c.Descricao}")
                                    .FontSize(12)
                                    .FontColor(Colors.Grey.Darken2);
                            }
                        });
                }
            });

            page.Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
        });
    }
}
