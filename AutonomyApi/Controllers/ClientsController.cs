using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Models.ViewModels.Client;
using AutonomyApi.Database;
using AutonomyApi.Models.Entities;

namespace AutonomyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        AutonomyDbContext _dbContext;
        WebServiceManager _ws;

        public ClientsController(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
            _ws = new WebServiceManager(this);
        }

        /// <summary>
        /// Search clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] ClientSearchView search)
        {
            return _ws.Perform(() =>
            {
                return Ok(new ClientService(_dbContext).Get(1, search));
            });
        }

        /// <summary>
        /// Find client by id
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return _ws.Perform(() =>
            {
                return Ok(new ClientService(_dbContext).Get(1, id));
            });
        }

        /// <summary>
        /// Register new client
        /// </summary>
        /// <param name="data">Client data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ClientCreationView data)
        {
            return _ws.Perform(() =>
            {
                var id = new ClientService(_dbContext).Create(1, data);

                return CreatedAtAction(nameof(Get), new { id }, new { id });
            });
        }

        /// <summary>
        /// Update client
        /// </summary>
        /// <param name="id">Client id</param>
        /// <param name="data">Client data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ClientUpdateView data)
        {
            return _ws.Perform(() =>
            {
                new ClientService(_dbContext).Update(1, id, data);
            });
        }

        /// <summary>
        /// Remove client
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _ws.Perform(() =>
            {
                new ClientService(_dbContext).Remove(1, id);
            });
        }

        /// <summary>
        /// Assign document to client
        /// </summary>
        /// <param name="id">Client id</param>
        /// <param name="data">Document</param>
        /// <returns></returns>
        [HttpPut("{id}/Documents")]
        public IActionResult PutDocument(int id, ClientDocumentUpdateView data)
        {
            return _ws.Perform(() =>
            {
                new ClientService(_dbContext).UpdateDocument(1, id, data);
            });
        }
    }
}