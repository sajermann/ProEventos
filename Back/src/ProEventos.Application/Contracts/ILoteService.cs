using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contracts
{
    public interface ILoteService
    {
        Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] model);
        Task<bool> DeleteLote(int eventoId, int loteId);

        Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId);

    }
}