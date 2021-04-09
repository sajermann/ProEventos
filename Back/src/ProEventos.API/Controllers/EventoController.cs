using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public IEnumerable<Evento> _evento =  new Evento[]{
                 new Evento{
                    EventoId = 1,
                    Tema = "Angular 11 e .Net 5", 
                    Local = "Belo Horizonte",
                    Lote = "1º Lote",
                    QtdPessoa = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString()
                 },
                new Evento{
                    EventoId = 2,
                    Tema = "Angular e suas Novidades", 
                    Local = "São Paulo",
                    Lote = "2º Lotes",
                    QtdPessoa = 350,
                    DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")
                 }   
            };
        public EventoController()
        {
           
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return _evento.Where(e=>e.EventoId == id);
        }

        [HttpPost]
        public string POst()
        {
            return "Exemplo Post";
        }
        
        [HttpPut("{id}")]
        public string Put(int id)
        {
            return "Exemplo Put com id = " + id;
        }
        
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return "Exemplo Delete = " + id;
        }
    }
}
