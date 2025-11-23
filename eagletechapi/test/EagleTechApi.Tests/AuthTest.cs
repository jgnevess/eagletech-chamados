using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eagletechapi.Contexts;
using eagletechapi.dto.usuario;
using eagletechapi.entity.usuario;
using eagletechapi.service.implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;


namespace EagleTechApi.Tests
{
    public class AuthTest
    {

        private AppDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        private UserIn CriarUsuario()
        {
            UserIn userIn = new()
            {
                NomeCompleto = "João Silva",
                Senha = "Senhapadrao1*",
                Telefone = "16993000000",
                Funcao = Funcao.ADMIN,
                Username = "joao@suporte"
            };

            return userIn;
        }

        [Fact]
        public async Task TestLoginReturnToken()
        {
            var usuarioIn = CriarUsuario();
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["Jwt:Key"]).Returns((string?)"12345678901234567890123456789012");

            var loggerMock = new Mock<ILogger<AuthService>>();
            var auth = new AuthService(context, mockConfig.Object, loggerMock.Object);

            await service.CadastrarUsuario(usuarioIn);
            var dto = new CredentialsLogin()
            {
                Username = "joao@suporte",
                Password = "Senhapadrao1*"
            };

            var res = await auth.Login(dto);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken((string)res.Token);

            Assert.Equal("joao@suporte", jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value);
            Assert.Equal("ADMIN", jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value);
            Assert.False(string.IsNullOrEmpty((string)res.Token));
            Assert.Equal("ADMIN", (string)res.Role);
            Assert.True((bool)res.FirstLogin);
        }

        [Fact]
        public async Task TestLoginShouldThrowExeptionNotFoundUser()
        {
            var usuarioIn = CriarUsuario();
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["Jwt:Key"]).Returns((string?)"12345678901234567890123456789012");


            var loggerMock = new Mock<ILogger<AuthService>>();
            var auth = new AuthService(context, mockConfig.Object, loggerMock.Object);

            await service.CadastrarUsuario(usuarioIn);
            var dto = new CredentialsLogin()
            {
                Username = "joao@suporte1",
                Password = "Senhapadrao1*"
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => auth.Login(dto));

            Assert.Equal("Usuário não encontrado, solicite o cadastro com um administrador", ex.Message);
        }

        [Fact]
        public async Task TestLoginShouldThrowExeptionincorrectPassword()
        {
            var usuarioIn = CriarUsuario();
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["Jwt:Key"]).Returns((string?)"12345678901234567890123456789012");


            var loggerMock = new Mock<ILogger<AuthService>>();
            var auth = new AuthService(context, mockConfig.Object, loggerMock.Object);

            await service.CadastrarUsuario(usuarioIn);
            var dto = new CredentialsLogin()
            {
                Username = "joao@suporte",
                Password = "SenhaSuperDificil123"
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => auth.Login(dto));

            Assert.Equal("Senha incorreta", ex.Message);
        }
    }
}