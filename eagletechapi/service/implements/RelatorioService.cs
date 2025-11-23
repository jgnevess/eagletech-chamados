using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eagletechapi.Contexts;
using eagletechapi.models.chamado;
using eagletechapi.models.chamado.enums;
using eagletechapi.service.interfaces;
using eagletechapi.utils;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace eagletechapi.service.implements
{
    public class RelatorioService
    {
        private readonly string _relatoriosDiretorio;
        private readonly AppDbContext _context;
        IWebHostEnvironment _environment;

        public RelatorioService(AppDbContext context, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _context = context;
            _environment = environment;
            _relatoriosDiretorio = configuration["Relatorios:Pasta"]
                          ?? Path.Combine(environment.ContentRootPath, "Relatorios");

            if (_environment.IsProduction())
            {
                _relatoriosDiretorio = "/app/relatorios"; // diretório dentro do container
            }

            if (!Directory.Exists(_relatoriosDiretorio))
            {
                Directory.CreateDirectory(_relatoriosDiretorio);
            }

        }
        private async Task<string> ChamadosAbertosPorPeriodo(FiltrosRelatorio filtrosRelatorio)
        {

            // Faz uma busca no banco de dados aplicando os filtros fornecidos

            var query = _context.Chamados
            .Include(c => c.Tecnico)
            .Include(c => c.Solicitante)
            .AsQueryable();

            // Aplica os filtros conforme fornecido e salva em uma lista

            if (filtrosRelatorio.Status.HasValue)
                query = query.Where(c => c.Status == filtrosRelatorio.Status.Value);

            if (filtrosRelatorio.DataInicio != default && filtrosRelatorio.DataFim != default)
                query = query.Where(c => c.Abertura >= filtrosRelatorio.DataInicio && c.Abertura <= filtrosRelatorio.DataFim);

            if (filtrosRelatorio.Categoria.HasValue)
                query = query.Where(c => c.Categoria == filtrosRelatorio.Categoria.Value);

            if (filtrosRelatorio.Prioridade.HasValue)
                query = query.Where(c => c.Prioridade == filtrosRelatorio.Prioridade.Value);

            var chamados = await query.ToListAsync();

            // Define o tipo de arquivo a ser gerado e chama o método correspondente e retorna o nome do arquivo gerado como string

            var nomeArquivo = "";
            if (filtrosRelatorio.ArquivoRelatorio.Equals(ArquivoRelatorio.CSV) || filtrosRelatorio.ArquivoRelatorio.Equals(ArquivoRelatorio.CSV_UTF8))
            {
                nomeArquivo = await CsvRelatorio(chamados, filtrosRelatorio);
            }
            else if (filtrosRelatorio.ArquivoRelatorio.Equals(ArquivoRelatorio.PDF))
            {
                nomeArquivo = await PdfRelatorio(chamados, filtrosRelatorio);
            }
            return nomeArquivo;
        }

        public async Task<RelatorioResponse> ObterArquivoRelatorio(FiltrosRelatorio filtrosRelatorio)
        {

            // Recebe os filtros e chama o método para gerar o relatório, obtendo o nome do arquivo gerado
            // Retorna um objeto RelatorioResponse contendo as informações do arquivo gerado

            var nomeArquivo = await ChamadosAbertosPorPeriodo(filtrosRelatorio);
            var caminhoCompleto = Path.Combine(_relatoriosDiretorio, nomeArquivo);
            return new RelatorioResponse
            {
                FileInfo = new FileInfo(caminhoCompleto),
                NomeArquivo = nomeArquivo
            };
        }

        private async Task<string> CsvRelatorio(List<Chamado> chamados, FiltrosRelatorio filtrosRelatorio)
        {

            // Gera o conteúdo do arquivo CSV com base nos chamados e filtros fornecidos e salva o arquivo no diretório especificado
            // Retorna o nome do arquivo gerado como string

            var nomeArquivo = $"relatorio_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            var caminhoCompleto = Path.Combine(_relatoriosDiretorio, nomeArquivo);

            var csvContent = new StringBuilder();

            if (filtrosRelatorio.ArquivoRelatorio.Equals(ArquivoRelatorio.CSV_UTF8))
            {
                csvContent.AppendLine($"\"Relatório de Chamados\",\"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm}\"");
            }
            else
            {
                csvContent.AppendLine($"Relatório de Chamados;Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm}");
            }
            csvContent.AppendLine($"\"Período: {filtrosRelatorio.DataInicio:dd/MM/yyyy} a {filtrosRelatorio.DataFim:dd/MM/yyyy}\"");
            csvContent.AppendLine($"\"Tipo de Relatório: {filtrosRelatorio.TipoRelatorio}\"");
            csvContent.AppendLine();

            if (filtrosRelatorio.TipoRelatorio.Equals(TipoRelatorio.Resumido))
            {
                if (filtrosRelatorio.ArquivoRelatorio.Equals(ArquivoRelatorio.CSV_UTF8))
                {
                    csvContent.AppendLine("Número do Chamado,Status,Solicitante,Técnico,Prioridade");
                }
                else
                {
                    csvContent.AppendLine("Número do Chamado;Status;Solicitante;Técnico;Prioridade");
                }
            }
            else
            {
                if (filtrosRelatorio.ArquivoRelatorio.Equals(ArquivoRelatorio.CSV_UTF8))
                {
                    csvContent.AppendLine("Número do Chamado,Status,Solicitante,Técnico,Prioridade,Abertura,Fechamento,Categoria,Descrição");
                }
                else
                {
                    csvContent.AppendLine("Número do Chamado;Status;Solicitante;Técnico;Prioridade;Abertura;Fechamento;Categoria;Descrição");
                }
            }

            foreach (var c in chamados)
            {
                var tecnico = c.Tecnico != null ? c.Tecnico.NomeCompleto : "Sem técnico";
                var fechamento = c.Fechamento.Year == 1 ? "Em Aberto" : c.Fechamento.ToString("dd/MM/yyyy HH:mm");

                if (filtrosRelatorio.TipoRelatorio.Equals(TipoRelatorio.Resumido))
                {
                    if (filtrosRelatorio.ArquivoRelatorio.Equals(ArquivoRelatorio.CSV_UTF8))
                    {
                        csvContent.AppendLine(
                            $"{c.NumeroChamado},{c.Status},{c.Solicitante!.NomeCompleto},{tecnico},{c.Prioridade}"
                        );
                    }
                    else
                    {
                        csvContent.AppendLine(
                            $"{c.NumeroChamado};{c.Status};{c.Solicitante!.NomeCompleto};{tecnico};{c.Prioridade}"
                        );
                    }
                }
                else
                {
                    if (filtrosRelatorio.ArquivoRelatorio.Equals(ArquivoRelatorio.CSV_UTF8))
                    {
                        var descricao = $"\"{c.Descricao.Replace("\"", "\"\"")}\"".Replace("\n", " ").Replace("\r", " ").Replace(",", ". ");
                        csvContent.AppendLine(
                            $"{c.NumeroChamado},{c.Status},{c.Solicitante!.NomeCompleto},{tecnico},{c.Prioridade},{c.Abertura:dd/MM/yyyy HH:mm},{fechamento},{c.Categoria},{descricao}"
                        );
                    }
                    else
                    {
                        csvContent.AppendLine(
                            $"{c.NumeroChamado};{c.Status};{c.Solicitante!.NomeCompleto};{tecnico};{c.Prioridade};{c.Abertura:dd/MM/yyyy HH:mm};{fechamento};{c.Categoria};{c.Descricao}"
                        );
                    }
                }
            }

            await File.WriteAllTextAsync(caminhoCompleto, csvContent.ToString(), Encoding.UTF8);
            return nomeArquivo;
        }


        private async Task<string> PdfRelatorio(List<Chamado> chamados, FiltrosRelatorio filtrosRelatorio)
        {
            // Gera o arquivo PDF com base nos chamados e filtros fornecidos e salva o arquivo no diretório especificado
            // O pdf é gerado usando a classe RelatorioPdf que implmenta uma biblioteca de geração de PDF "IDocument"
            // Retorna o nome do arquivo gerado como string
            var relatorio = new RelatorioPdf(chamados, filtrosRelatorio);
            var nomeArquivo = $"relatorio_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            var caminho = Path.Combine(_relatoriosDiretorio, nomeArquivo);
            relatorio.GeneratePdf(caminho);
            return nomeArquivo;
        }
    }

}