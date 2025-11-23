using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eagletechapi.dto.usuario;
using eagletechapi.service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/*
  Métodos dos controllers responsáveis por definir os endpoints da API,
  conectando as requisições do frontend com as regras de negócio da aplicação.
  Essa classe é resposavel pelos endpoints de autenticação.
*/

namespace eagletechapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService service, IUserService userService, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IAuthService _authService = service;
        private readonly IUserService _userService = userService;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialsLogin dto)
        {
            if (!ModelState.IsValid) return BadRequest(new Dictionary<string, string>()
            {
                { "Error", "Por favor, preencha todos os campos" },
            });
            try
            {
                var res = await _authService.Login(dto);
                return Ok(res);

            }
            catch (Exception e)
            {
                var res = new Dictionary<string, string>
                {
                    { "Error", e.Message }
                };
                _logger.LogError("Falha no login: usuário: {DtoUsername}", dto.Username);
                if (e.Message.Contains("senha"))
                {
                    _logger.LogWarning("Senha incorreta: usuário: {DtoUsername}", dto.Username);
                }
                return BadRequest(res);
            }
        }

        [HttpPost("register")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Register([FromBody] UserIn userIn)
        {
            if (!ModelState.IsValid) return BadRequest(new Dictionary<string, string>()
            {
                { "Error", "Por favor, preencha todos os campos" },
            });
            try
            {
                var res = await _userService.CadastrarUsuario(userIn);
                _logger.LogInformation("Cadastro de usuário: {ResNomeCompleto}, {resFuncao}", res.NomeCompleto, res.Funcao);
                return Ok(res);
            }
            catch (Exception e)
            {
                var res = new Dictionary<string, string>
                {
                    { "Error", e.Message }
                };
                _logger.LogError("Falha no cadastro: {EMessage}", e.Message);
                return BadRequest(res);
            }
        }
    }
}