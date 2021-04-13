using System;
using System.Threading.Tasks;
using ProEventos.Application.Contracts;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _eventoPersist = eventoPersist;
            _geralPersist = geralPersist;

        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try{
                _geralPersist.Add<Evento>(model);
               if(await _geralPersist.SaveChangesAsync()){
                   return await _eventoPersist.GetAllEventoByIdAsync(model.Id, false);
               };
               return null;
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try{
                var evento = await _eventoPersist.GetAllEventoByIdAsync(eventoId, false);
                if(evento == null) return null;

                model.Id = evento.Id;

                _geralPersist.Update(model);
                //var t = await _geralPersist.SaveChangesAsync();
                
                if(await _geralPersist.SaveChangesAsync()){
                   return await _eventoPersist.GetAllEventoByIdAsync(model.Id, false);
               };
               return null;
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteEventos(int eventoId)
        {
            try{
                var evento = await _eventoPersist.GetAllEventoByIdAsync(eventoId, false);
                if(evento == null) throw new Exception("Evento para delete não encontrado");

                _geralPersist.Delete<Evento>(evento);
                return await _geralPersist.SaveChangesAsync();
               
            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Evento> GetAllEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventoByIdAsync(eventoId, includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}