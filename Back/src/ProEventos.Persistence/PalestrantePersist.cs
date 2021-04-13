using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;
using ProEventos.Persistence.Context;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Palestrante> GetAllPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                
                .Include(e=>e.RedesSociais);

                if(includeEventos){
                    query = query.Include(e=>e.PalestrantesEventos)
                        .ThenInclude(e=>e.Evento);
                }

                query = query.OrderBy(e=>e.Id)
                    .Where(e=>e.Id == palestranteId);
                return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                
                .Include(e=>e.RedesSociais);

                if(includeEventos){
                    query = query.Include(e=>e.PalestrantesEventos)
                        .ThenInclude(e=>e.Evento);
                }

                query = query.OrderBy(e=>e.Id);
                return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                
                .Include(e=>e.RedesSociais);

                if(includeEventos){
                    query = query.Include(e=>e.PalestrantesEventos)
                        .ThenInclude(e=>e.Evento);
                }

                query = query.OrderBy(e=>e.Id)
                    .Where(e=>e.Nome.ToLower().Contains(nome.ToLower()));
                return await query.ToArrayAsync();
        }
    }
}