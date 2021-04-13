using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Nenhum Evento Encontrado");
                return Ok(eventos);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetAllEventoByIdAsync(id, true);
                if(evento == null) return NotFound("Evento por Id não encontrados.");
                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if(evento == null) return NotFound("Eventos por tema não encontrados.");
                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEventos(model);
                if(evento == null) return BadRequest("Erro ao tentar adicionar eventos.");
                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar eventos. Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if(evento == null) return BadRequest("Erro ao tentar atualizar eventos.");
                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar eventos. Erro: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if(await _eventoService.DeleteEventos(id))
                    return Ok("Deletado");
                else
                    return BadRequest("Evento Não Deletado");
                
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir eventos. Erro: {e.Message}");
            }
        }
    }
}
