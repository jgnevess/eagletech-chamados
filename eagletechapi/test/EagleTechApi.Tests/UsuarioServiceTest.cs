using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using eagletechapi.Contexts;
using Microsoft.EntityFrameworkCore;
using eagletechapi.models.usuario;
using eagletechapi.dto.usuario;
using eagletechapi.service.implements;
using System.ComponentModel.DataAnnotations;
using eagletechapi.entity.usuario;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eagletechapi.test.service
{
    public class UsuarioServiceTest
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
                Funcao = entity.usuario.Funcao.ADMIN,
                Username = "joao@testeemail.com"
            };

            return userIn;
        }

        [Fact]
        public async Task CreateUserReturnSuccess()
        {
            var usuarioIn = CriarUsuario();
            var context = GetInMemoryDb();
            var service = new UserService(context);

            await service.CadastrarUsuario(usuarioIn);

            var res = await context.Usuarios.FindAsync(1);

            Assert.NotNull(res);
            Assert.Equal("João Silva", res.NomeCompleto);
            Assert.NotEqual("Senhapadrao1*", res.Senha);
        }

        [Fact]
        public async Task CreateUserThrowExceptionName()
        {
            var usuarioIn = CriarUsuario();
            usuarioIn.NomeCompleto = "Jo";

            var context = GetInMemoryDb();
            var service = new UserService(context);

            var ex = await Assert.ThrowsAsync<ValidationException>(() => service.CadastrarUsuario(usuarioIn));

            Assert.Equal("O nome deve ter entre 3 e 40 caracteres", ex.Message);
        }

        [Fact]
        public async Task CreateUserThrowExceptionNameRequired()
        {
            var usuarioIn = CriarUsuario();
            usuarioIn.NomeCompleto = "";

            var context = GetInMemoryDb();
            var service = new UserService(context);

            var ex = await Assert.ThrowsAsync<ValidationException>(() => service.CadastrarUsuario(usuarioIn));

            Assert.Equal("O nome é obrigatório", ex.Message);
        }

        [Fact]
        public async Task CreateUserThrowExceptionEmail()
        {
            var usuarioIn = CriarUsuario();
            usuarioIn.Username = "Jo";

            var context = GetInMemoryDb();
            var service = new UserService(context);

            var ex = await Assert.ThrowsAsync<ValidationException>(() => service.CadastrarUsuario(usuarioIn));

            Assert.Equal("O username não é valido", ex.Message);
        }

        [Fact]
        public async Task CreateUserThrowExceptionUsernameUnique()
        {

            var context = GetInMemoryDb();
            var service = new UserService(context);


            var usuarioIn = CriarUsuario();
            await service.CadastrarUsuario(usuarioIn);

            var ex = await Assert.ThrowsAsync<Exception>(
                () => service.CadastrarUsuario(usuarioIn)
            );

            Assert.Equal("Username já cadastrado", ex.Message);
        }

        [Fact]
        public async Task CreateUserThrowExceptionEmailIsRequired()
        {
            var usuarioIn = CriarUsuario();
            usuarioIn.Username = "";

            var context = GetInMemoryDb();
            var service = new UserService(context);

            var ex = await Assert.ThrowsAsync<ValidationException>(() => service.CadastrarUsuario(usuarioIn));

            Assert.Equal("The Username field is required.", ex.Message);
        }

        [Fact]
        public async Task CreateUserThrowExceptionTelefone()
        {
            var usuarioIn = CriarUsuario();
            usuarioIn.Telefone = "123";

            var context = GetInMemoryDb();
            var service = new UserService(context);

            var ex = await Assert.ThrowsAsync<ValidationException>(() => service.CadastrarUsuario(usuarioIn));

            Assert.Equal("O telefone deve ter 11 caracteres", ex.Message);
        }

        [Fact]
        public async Task CreateUserThrowExceptionTelefoneIsRequired()
        {
            var usuarioIn = CriarUsuario();
            usuarioIn.Telefone = "";

            var context = GetInMemoryDb();
            var service = new UserService(context);

            var ex = await Assert.ThrowsAsync<ValidationException>(() => service.CadastrarUsuario(usuarioIn));

            Assert.Equal("The Telefone field is required.", ex.Message);
        }

        [Fact]
        public async Task CreateUserThrowExceptionTelefoneApenasNumeros()
        {
            var usuarioIn = CriarUsuario();
            usuarioIn.Telefone = "as112334321";

            var context = GetInMemoryDb();
            var service = new UserService(context);

            var ex = await Assert.ThrowsAsync<ValidationException>(() => service.CadastrarUsuario(usuarioIn));

            Assert.Equal("O telefone só pode conter números", ex.Message);
        }

        [Fact]
        public async Task FindAllUsersShouldReturnEmpityList()
        {
            var context = GetInMemoryDb();
            var service = new UserService(context);

            var res = await service.ListarTodos(1, 1);

            Assert.Empty(res.Data);
        }

        [Fact]
        public async Task FindAllUsersShouldReturnListWithItem()
        {
            var usuarioIn = CriarUsuario();
            var context = GetInMemoryDb();
            var service = new UserService(context);

            await service.CadastrarUsuario(usuarioIn);

            var res = await service.ListarTodos(1, 1);

            Assert.NotEmpty(res.Data);
        }

        [Fact]
        public async Task FindUsersShouldThrowException()
        {

            var context = GetInMemoryDb();
            var service = new UserService(context);

            var ex = await Assert.ThrowsAsync<Exception>(() => service.BuscarUsuario(1));

            Assert.Equal("Usuario não encontrado", ex.Message);

        }

        [Fact]
        public async Task FindUsersShouldNotNull()
        {

            var context = GetInMemoryDb();
            var service = new UserService(context);
            var usuarioIn = CriarUsuario();
            await service.CadastrarUsuario(usuarioIn);

            var res = await service.BuscarUsuario(1);

            Assert.NotNull(res);
            Assert.Equal("João Silva", res.NomeCompleto);
        }

        [Fact]
        public async Task FindUsersByNameShouldNull()
        {
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var res = await service.BuscarUsuario("joao");

            Assert.Empty(res);
        }

        [Fact]
        public async Task FindUsersByNameShouldNotNull()
        {

            var context = GetInMemoryDb();
            var service = new UserService(context);
            var usuarioIn = CriarUsuario();
            await service.CadastrarUsuario(usuarioIn);

            var res = await service.BuscarUsuario("Jo");

            Assert.NotNull(res);
            Assert.Equal("João Silva", res[0].NomeCompleto);
        }

        [Fact]
        public async Task UpdatePasswordShouldThrowNotFoundExeption()
        {

            var context = GetInMemoryDb();
            var service = new UserService(context);
            var up = new PasswordUpdate(1, "Jo", "xyz.", "xyz.");
            var sup = new SimplePasswordUpdate(1, "xyz.", "xyz.");

            var ex = await Assert.ThrowsAsync<Exception>(() => service.AlterarSenha(sup));
            var ex2 = await Assert.ThrowsAsync<Exception>(() => service.AlterarSenha(up));

            Assert.Equal("Usuário não encontrado", ex.Message);
            Assert.Equal("Usuário não encontrado", ex2.Message);
        }

        [Fact]
        public async Task UpdatePasswordShouldThrowIncorrectPasswordExeption()
        {
            var up = new PasswordUpdate(1, "Jo", "xyz.", "xyz.");
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var usuarioIn = CriarUsuario();
            await service.CadastrarUsuario(usuarioIn);


            var ex = await Assert.ThrowsAsync<Exception>(() => service.AlterarSenha(up));
            Assert.Equal("Senha incorreta", ex.Message);
        }

        [Fact]
        public async Task UpdatePasswordShouldThrowValidationExeption()
        {
            var up = new PasswordUpdate(1, "Jo", "xyz.", "xyz.");
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var usuarioIn = CriarUsuario();
            await service.CadastrarUsuario(usuarioIn);


            var ex = await Assert.ThrowsAsync<Exception>(() => service.AlterarSenha(up));
            Assert.Equal("Senha incorreta", ex.Message);
        }

        [Fact]
        public async Task UpdatePasswordShouldThrowEqualsPasswordExeption()
        {

            var up = new PasswordUpdate(1, "Senhapadrao1*", "Senhapadrao1*", "Senhapadrao1*");
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var usuarioIn = CriarUsuario();
            await service.CadastrarUsuario(usuarioIn);


            var ex = await Assert.ThrowsAsync<Exception>(() => service.AlterarSenha(up));
            Assert.Equal("A nova senha precisa ser diferente da antiga", ex.Message);
        }

        [Fact]
        public async Task UpdatePasswordShouldReturnSuccess()
        {

            var up = new PasswordUpdate(1, "Senhapadrao1*", "NovaSenhaDificil321.", "NovaSenhaDificil321.");
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var usuarioIn = CriarUsuario();
            await service.CadastrarUsuario(usuarioIn);


            var res = await service.AlterarSenha(up);
            Assert.NotNull(res);
        }

        [Fact]
        public async Task UpdatePasswordByAdminShouldReturnSuccess()
        {

            var context = GetInMemoryDb();
            var service = new UserService(context);
            var usuarioIn = CriarUsuario();
            var sup = new SimplePasswordUpdate(1, "NovaSenhaDificil321.", "NovaSenhaDificil321.");
            await service.CadastrarUsuario(usuarioIn);


            var res = await service.AlterarSenha(sup);
            Assert.NotNull(res);
        }

        [Fact]
        public async Task DeleteUserShouldThrowException()
        {
            var context = GetInMemoryDb();
            var service = new UserService(context);

            var ex = await Assert.ThrowsAsync<Exception>(() => service.DeletarUsuario(1));

            Assert.Equal("Usuario não encontrado", ex.Message);
        }

        [Fact]
        public async Task DeleteUserShouldSuccess()
        {
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var usuarioIn = CriarUsuario();
            await service.CadastrarUsuario(usuarioIn);

            var res = await service.BuscarUsuario(1);
            Assert.True(res.Ativo);

            await service.DeletarUsuario(1);

            res = await service.BuscarUsuario(1);

            Assert.False(res.Ativo);
        }

        [Fact]
        public async Task UpdateUserShouldThrowException()
        {
            var context = GetInMemoryDb();
            var service = new UserService(context);

            var ex = await Assert.ThrowsAsync<Exception>(() => service.EditarUsuario(1, new UserUpdateIn()));

            Assert.Equal("Usuario não encontrado", ex.Message);
        }

        [Fact]
        public async Task UpdateUserShouldSuccess()
        {
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var usuarioIn = CriarUsuario();
            await service.CadastrarUsuario(usuarioIn);

            int matricula = 1;
            string nome = "João Gabriel Silva";
            string telefone = "16993000001";
            Funcao funcao = Funcao.TECNICO;

            var res = await service.EditarUsuario(matricula, new UserUpdateIn()
            {
                Matricula = matricula,
                NomeCompleto = nome,
                Telefone = telefone,
                Funcao = funcao,
            });

            Assert.NotNull(res);
            Assert.Equal(matricula, res.Matricula);
            Assert.Equal(nome, res.NomeCompleto);
            Assert.Equal(funcao, res.Funcao);
        }

        [Fact]
        public async Task UpdateUserShouldThrowValidadeException()
        {
            var context = GetInMemoryDb();
            var service = new UserService(context);
            var usuarioIn = CriarUsuario();
            await service.CadastrarUsuario(usuarioIn);

            int matricula = 1;
            string nome = "";
            string telefone = "16993000001";
            Funcao funcao = Funcao.TECNICO;
            string email = "joaoteste@testeemail.com";



            var ex = await Assert.ThrowsAsync<ValidationException>(() => service.
                EditarUsuario(matricula,
                    new UserUpdateIn()
                    {
                        Matricula = matricula,
                        NomeCompleto = nome,
                        Telefone = telefone,
                        Funcao = funcao
                    }
                )
            );

            Assert.Equal("O nome é obrigatório", ex.Message);
        }
    }
}