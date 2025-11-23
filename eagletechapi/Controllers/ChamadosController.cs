using eagletechapi.dto.chamado;
using eagletechapi.entity.chamado.enums;
using eagletechapi.models.chamado.enums;
using eagletechapi.service.implements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

/*
  Métodos dos controllers responsáveis por definir os endpoints da API,
  conectando as requisições do frontend com as regras de negócio da aplicação.
  Essa classe é resposavel pelos endpoints de chamados.
*/

namespace eagletechapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController(ChamadoService chamadoService) : ControllerBase
    {

        private string TratarExeption(ModelStateDictionary stateDictionary)
        {
            return ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault() ?? "Erro na validação";
        }

        [HttpPost("abrir-chamado")]
        [Authorize(Roles = "SOLICITANTE")]
        public async Task<IActionResult> AbrirChamado([FromBody] ChamadoIn chamadoIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Dictionary<string, string>()
                {
                    { "Error", TratarExeption(ModelState) }
                });
            }

            try
            {
                var res = await chamadoService.AbrirChamado(chamadoIn);

                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("chamados-abertos")]
        public async Task<IActionResult> BuscarChamadosAbertos(int pageSize, int page)
        {
            return Ok(await chamadoService.BuscarChamadosSolicitante(Status.ABERTO, pageSize, page));
        }

        [Authorize]
        [HttpGet("chamados")]
        public async Task<IActionResult> BuscarChamadoPorStatus(Status status, int pageSize, int page)
        {
            return Ok(await chamadoService.BuscarChamadosSolicitante(status, pageSize, page));
        }

        [Authorize(Roles = "SOLICITANTE")]
        [HttpGet("chamados-solicitante")]
        public async Task<IActionResult> BuscarChamadoPorStatusESolcitante(int solicitante, Status status, int pageSize, int page)
        {
            return Ok(await chamadoService.BuscarChamadosSolicitante(solicitante, status, pageSize, page));
        }

        [Authorize(Roles = "TECNICO")]
        [HttpPut("aceitar-tecnico")]
        public async Task<IActionResult> AceitarChamado(int numeroChamado, int tecnicoMatricula)
        {
            try
            {
                var res = await chamadoService.AceitarChamado(numeroChamado, tecnicoMatricula);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "TECNICO")]
        [HttpPut("fechar-tecnico")]
        public async Task<IActionResult> FinalizarChamado([FromBody] Fechamento fechamento)
        {
            try
            {
                var res = await chamadoService.FecharChamado(fechamento);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "TECNICO")]
        [HttpGet("chamados-tecnico")]
        public async Task<IActionResult> BuscarChamadoPorStatusETecnico(int tecnico, Status status, int pageSize, int page)
        {
            return Ok(await chamadoService.BuscarChamadosTecnico(tecnico, status, pageSize, page));
        }

        [Authorize]
        [HttpGet("chamado")]
        public async Task<IActionResult> BuscarChamadoPorId(int numeroChamado)
        {
            var res = await chamadoService.BuscarChamado(numeroChamado);
            return Ok(res);
        }

        [Authorize(Roles = "SOLICITANTE")]
        [HttpDelete("cancelar-chamado")]
        public async Task<IActionResult> CancelarChamado(int numeroChamado)
        {
            try
            {
                await chamadoService.DeletarChamado(numeroChamado);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "SOLICITANTE")]
        [HttpPut("editar-chamado")]
        public async Task<IActionResult> EditarChamado(int numeroChamado, ChamadoIn chamadoIn)
        {
            try
            {
                var res = await chamadoService.EditarChamado(numeroChamado, chamadoIn);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("reabir-chamado")]
        public async Task<IActionResult> ReabrirChamado(int numeroChamado)
        {
            try
            {
                var res = await chamadoService.ReabrirChamado(numeroChamado);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}