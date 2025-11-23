using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eagletechapi.service.implements;
using eagletechapi.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/*
  Métodos dos controllers responsáveis por definir os endpoints da API,
  conectando as requisições do frontend com as regras de negócio da aplicação.
  Essa classe é resposavel pelos endpoints de relatórios.
*/

namespace eagletechapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController(RelatorioService relatorioService) : ControllerBase
    {

        [Authorize(Roles = "ADMIN")]
        [HttpPost("download-periodo")]
        public async Task<IActionResult> DownloadRelatorio([FromBody] FiltrosRelatorio filtrosRelatorio)
        {
            try
            {
                var res = await relatorioService.ObterArquivoRelatorio(filtrosRelatorio);
                var arquivo = res.FileInfo;

                if (arquivo == null || !arquivo.Exists)
                {
                    return NotFound(new
                    {
                        sucesso = false,
                        mensagem = "Arquivo não encontrado"
                    });
                }

                var contentType = arquivo.Extension.ToLower() switch
                {
                    ".pdf" => "application/pdf",
                    ".csv" => "text/csv",
                    _ => "application/octet-stream"
                };

                var stream = new FileStream(arquivo.FullName, FileMode.Open, FileAccess.Read);
                return File(stream, contentType, arquivo.Name);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    sucesso = false,
                    mensagem = $"Erro ao baixar relatório: {ex.Message}"
                });
            }
        }

    }
}