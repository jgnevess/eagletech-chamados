using eagletechapi.Contexts;
using eagletechapi.dto.chamado;
using eagletechapi.dto.usuario;
using eagletechapi.entity.chamado.enums;
using eagletechapi.entity.usuario;
using eagletechapi.models.chamado.enums;
using eagletechapi.models.usuario;
using eagletechapi.service.implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace eagletechapi.test.EagleTechApi.Tests
{
    public class ChamadoTest
    {

        private static ChamadoIn CriarChamado(Usuario usuario)
        {
            var chamadoIn = new ChamadoIn()
            {
                Titulo = "Internet lenta",
                Descricao = "Minha internet está lenta",
                Categoria = Categoria.REDE,
                UsuarioId = usuario.Matricula
            };
            return chamadoIn;
        }

        private static AppDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        private static UserIn CriarUsuarioSolicitante()
        {
            UserIn userIn = new()
            {
                NomeCompleto = "João Sol",
                Senha = "SenhaSuperDificil123*",
                Telefone = "16993000000",
                Funcao = Funcao.SOLICITANTE,
                Username = "joao@sol.com"
            };
            return userIn;
        }

        private static UserIn CriarUsuarioTecnico()
        {
            UserIn userIn = new()
            {
                NomeCompleto = "João Tec",
                Senha = "SenhaSuperDificil123*",
                Telefone = "16993000000",
                Funcao = Funcao.TECNICO,
                Username = "joao@tec.com"
            };
            return userIn;
        }

        [Fact]
        public async Task CreateChamadoShouldReturnSuccess()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var us = context.Usuarios.Add(u);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            var res = await service.AbrirChamado(CriarChamado(us.Entity));

            Assert.NotNull(res);
            Assert.Equal("Internet lenta", res.Titulo);
            Assert.Equal("Minha internet está lenta", res.Descricao);
            Assert.Equal(Categoria.REDE, res.Categoria);
            Assert.Equal("João Sol", res.Solicitante.NomeCompleto);
            Assert.Equal(Status.ABERTO, res.Status);
        }

        [Fact]
        public async Task BuscarChamadoShouldReturnSuccess()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var us = context.Usuarios.Add(u);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));

            var res = await service.BuscarChamado(1);
            Assert.NotNull(res);
            Assert.Equal(Status.ABERTO, res.Status);
        }

        [Fact]
        public async Task BuscarChamadoShouldThrowException()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var us = context.Usuarios.Add(u);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await Assert.ThrowsAsync<Exception>(() => service.BuscarChamado(1));
        }

        [Fact]
        public async Task BuscarChamadosComStatusShouldReturnList()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var us = context.Usuarios.Add(u);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));

            var res = await service.BuscarChamadosSolicitante(Status.ABERTO, 1, 0);
            Assert.Single(res.Data);
            Assert.Equal(Status.ABERTO, res.Data.First().Status);
        }

        [Fact]
        public async Task BuscarChamadosComStatusEUsuarioShouldReturnList()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var us = context.Usuarios.Add(u);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));

            var res = await service.BuscarChamadosSolicitante(1, Status.ABERTO, 1, 0);

            Assert.Single(res.Data);
            Assert.Equal(Status.ABERTO, res.Data.First().Status);
        }

        [Fact]
        public async Task UpdateChamadoShouldReturnSuccess()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var us = context.Usuarios.Add(u);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));

            var chamado = CriarChamado(us.Entity);
            chamado.Titulo = "Novo Titulo";
            chamado.Descricao = "Nova Descricao";
            chamado.Categoria = Categoria.BANCO_DE_DADOS;
            var res = await service.EditarChamado(1, chamado);

            Assert.NotNull(res);
            Assert.Equal("Novo Titulo", res.Titulo);
            Assert.Equal("Nova Descricao", res.Descricao);
            Assert.Equal(Categoria.BANCO_DE_DADOS, res.Categoria);
            Assert.Equal("João Sol", res.Solicitante.NomeCompleto);
            Assert.Equal(Status.ABERTO, res.Status);
        }

        [Fact]
        public async Task UpdateChamadoShouldThrowExeption()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var us = context.Usuarios.Add(u);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));

            var chamado = CriarChamado(us.Entity);
            chamado.Titulo = "Novo Titulo";
            chamado.Descricao = "Nova Descricao";
            chamado.Categoria = Categoria.BANCO_DE_DADOS;
            await Assert.ThrowsAsync<Exception>(() => service.EditarChamado(2, chamado));
        }

        [Fact]
        public async Task AceitarChamadoShouldReturnSuccess()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var tec = CriarUsuarioTecnico();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var ut = new Usuario(tec);
            var us = context.Usuarios.Add(u);
            var tecE = context.Usuarios.Add(ut);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));
            var res = await service.AceitarChamado(1, tecE.Entity.Matricula);

            Assert.NotNull(res);
            Assert.Equal(Status.EM_ANDAMENTO, res.Status);
            Assert.Equal(2, res.Tecnico.Matricula);
        }

        [Fact]
        public async Task AceitarChamadoShouldThrowExeption()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var tec = CriarUsuarioTecnico();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var ut = new Usuario(tec);
            var us = context.Usuarios.Add(u);
            var tecE = context.Usuarios.Add(ut);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));

            await Assert.ThrowsAsync<Exception>(() => service.AceitarChamado(99, tecE.Entity.Matricula));
        }

        [Fact]
        public async Task FecharChamadoShouldReturnSuccess()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var tec = CriarUsuarioTecnico();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var ut = new Usuario(tec);
            var us = context.Usuarios.Add(u);
            var tecE = context.Usuarios.Add(ut);
            var fechamento = new Fechamento(1, tecE.Entity.Matricula, "Resolvido com modem reiniciado");
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));
            await service.AceitarChamado(1, tecE.Entity.Matricula);

            var res = await service.FecharChamado(fechamento);

            Assert.NotNull(res);
            Assert.Equal(Status.FECHADO, res.Status);
            Assert.Equal(2, res.Tecnico.Matricula);
        }

        [Fact]
        public async Task FecharChamadoShouldThrowExeption()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var tec = CriarUsuarioTecnico();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var ut = new Usuario(tec);
            var us = context.Usuarios.Add(u);
            var tecE = context.Usuarios.Add(ut);
            var fechamento = new Fechamento(1, tecE.Entity.Matricula, "");
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));

            await Assert.ThrowsAsync<Exception>(() => service.FecharChamado(fechamento));
        }

        [Fact]
        public async Task DeletarChamadoChamadoShouldThrowExeption()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var tec = CriarUsuarioTecnico();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var ut = new Usuario(tec);
            var us = context.Usuarios.Add(u);
            var tecE = context.Usuarios.Add(ut);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));
            await service.AceitarChamado(1, tecE.Entity.Matricula);

            await Assert.ThrowsAsync<Exception>(() => service.DeletarChamado(1));
        }

        [Fact]
        public async Task DeletarChamadoChamadoShouldReturnAEmptyList()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var tec = CriarUsuarioTecnico();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var ut = new Usuario(tec);
            var us = context.Usuarios.Add(u);
            var tecE = context.Usuarios.Add(ut);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));
            await service.DeletarChamado(1);

            Assert.Empty(context.Chamados);
        }

        [Fact]
        public async Task DeletarChamadoChamadoShouldThrowAExeption()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var tec = CriarUsuarioTecnico();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var ut = new Usuario(tec);
            var us = context.Usuarios.Add(u);
            var tecE = context.Usuarios.Add(ut);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await Assert.ThrowsAsync<Exception>(() => service.DeletarChamado(1));
        }

        [Fact]
        public async Task ReabrirChamadoShouldReturnSuccess()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var tec = CriarUsuarioTecnico();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var ut = new Usuario(tec);
            var us = context.Usuarios.Add(u);
            var tecE = context.Usuarios.Add(ut);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);
            var fechamento = new Fechamento(1, tecE.Entity.Matricula, "Resolvido com modem reiniciado");

            await service.AbrirChamado(CriarChamado(us.Entity));
            await service.AceitarChamado(1, tecE.Entity.Matricula);
            await service.FecharChamado(fechamento);

            var res = await service.ReabrirChamado(1);

            Assert.NotNull(res);
            Assert.Equal(Status.ABERTO, res.Status);
            Assert.Null(res.Tecnico);
        }

        [Fact]
        public async Task ReabirChamadoChamadoShouldThrowExeption()
        {
            var loggerMock = new Mock<ILogger<ChamadoService>>();
            var usuario = CriarUsuarioSolicitante();
            var tec = CriarUsuarioTecnico();
            var context = GetInMemoryDb();
            var u = new Usuario(usuario);
            var ut = new Usuario(tec);
            var us = context.Usuarios.Add(u);
            var tecE = context.Usuarios.Add(ut);
            await context.SaveChangesAsync();
            var service = new ChamadoService(context, loggerMock.Object);

            await service.AbrirChamado(CriarChamado(us.Entity));
            await service.AceitarChamado(1, tecE.Entity.Matricula);

            await Assert.ThrowsAsync<Exception>(() => service.ReabrirChamado(1));
        }
    }


}