using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eagletechapi.dto;
using eagletechapi.dto.chamado;
using eagletechapi.models.chamado.enums;

namespace eagletechapi.service.interfaces
{
    public interface IChamadoService
    {
        Task<ChamadoOut> AbrirChamado(ChamadoIn chamadoIn);
        Task<ChamadoOut?> BuscarChamado(int numeroChamado);
        Task<ResponseList<ChamadoOut>> BuscarChamadosSolicitante(Status status, int pageSize, int page);
        Task<ResponseList<ChamadoOut>> BuscarChamadosSolicitante(int usuarioId, Status status, int pageSize, int page);
        Task<ResponseList<ChamadoOut>> BuscarChamadosTecnico(int usuarioId, Status status, int pageSize, int page);
        Task<ChamadoOut?> EditarChamado(int numeroChamado, ChamadoIn chamadoIn);
        Task<ChamadoOut?> AceitarChamado(int numeroChamado, int tecnicoId);
        Task<ChamadoOut?> FecharChamado(Fechamento fechamento);
        Task<ChamadoOut?> ReabrirChamado(int numeroChamado);
        Task DeletarChamado(int numeroChamado);


    }
}