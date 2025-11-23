using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eagletechapi.dto;
using eagletechapi.dto.usuario;
using eagletechapi.models.usuario;

namespace eagletechapi.service.interfaces
{
    public interface IUserService
    {
        Task<UserOut> CadastrarUsuario(UserIn userIn);
        Task<UserOut?> AlterarSenha(SimplePasswordUpdate simplePasswordUpdate);
        Task<UserOut?> AlterarSenha(PasswordUpdate passwordUpdate);
        Task<ResponseList<Usuario>> ListarTodos(int pageNumber, int pageSize);
        Task<UserOut?> BuscarUsuario(int matricula);
        Task<List<UserOut>> BuscarUsuario(string nome);
        Task<UserOut> EditarUsuario(int matricula, UserUpdateIn userIn);
        Task DeletarUsuario(int matricula);
        Task AtivarUsuario(int matricula);
        Task<UserOut?> ResetPassword(int matricula);
        Task<UserOut> AlterarTema(int matricula, Theme theme);
    }
}