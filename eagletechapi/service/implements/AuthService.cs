using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using eagletechapi.Contexts;
using eagletechapi.dto.usuario;
using eagletechapi.service.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;



namespace eagletechapi.service.implements
{
    public class AuthService(AppDbContext context, IConfiguration configuration, ILogger<AuthService> logger) : IAuthService
    {
        public async Task<LoginResponse> Login(CredentialsLogin credentialsLogin)
        {
            // Busca o usuário no banco de dados pela matrícula recebida no LoginDto
            // Caso não encontre, lança uma exceção

            var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Username.Equals(credentialsLogin.Username)) ??
            throw new Exception("Usuário não encontrado, solicite o cadastro com um administrador");

            if (!usuario.Ativo) throw new Exception("Usuário não encontrado, solicite o cadastro com um administrador");

            // Compara o hash da senha recebida do frontend com o hash do banco de dados
            // Se não forem iguais, lança uma exceção

            if (!BCrypt.Net.BCrypt.Verify(credentialsLogin.Password, usuario.Senha)) throw new Exception("Senha incorreta");


            // Cria um array de claims (informações do usuário) que serão incluídas no token
            // Essas claims serão usadas depois para validação de permissões e identidade

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.Funcao.ToString())
            };

            // Busca a chave da API no arquivo de configuração

            var apiKey = configuration["Jwt:Key"] ?? "";

            // Cria as credenciais de assinatura do token usando a chave e o algoritmo HMAC-SHA256

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(apiKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Cria as credenciais de assinatura do token usando a chave e o algoritmo HMAC-SHA256

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            logger.LogInformation($"Login do usuário: {usuario.Username}\nValidade do token: {DateTime.Now.AddHours(3)}");

            // Retorna a resposta para o controller com o token e informações do usuário

            return new LoginResponse(
                new JwtSecurityTokenHandler().WriteToken(token),
                usuario.Funcao.ToString(),
                usuario.Matricula,
                new UserOut(usuario),
                usuario.FirstLogin
                );
        }
    }
}