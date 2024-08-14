using AutonomyApi.Database;
using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Models.Dtos;
using AutonomyApi.Models.Dtos.Client;
using AutonomyApi.Models.Views.Client;
using AutonomyApi.Models.Entities;
using AutonomyApi.Enums;

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
        /// List all clients
        /// </summary>
        /// <param name="search">Name filter (optional)</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string? search)
        {
            return _ws.Perform(() =>
            {
                return Ok(new ClientService(_dbContext).Get(1, search));
            });
        }

        /// <summary>
        /// Find a client by id
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
        /// Register a new client
        /// </summary>
        /// <param name="data">Client data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ClientCreationData data)
        {
            return _ws.Perform(() =>
            {
                var id = new ClientService(_dbContext).Create(1, data);

                return CreatedAtAction(nameof(Get), new { id }, new { id });
            });
        }

        /// <summary>
        /// Update a client
        /// </summary>
        /// <param name="id">Client id</param>
        /// <param name="data">Client data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ClientUpdateData data)
        {
            return _ws.Perform(() =>
            {
                new ClientService(_dbContext).Update(1, id, data);
            });
        }

        /// <summary>
        /// Remove a client
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
        /// Assign a document to a client
        /// </summary>
        /// <param name="id">Client id</param>
        /// <param name="type">Document type</param>
        /// <param name="data">Document</param>
        /// <returns></returns>
        [HttpPut("{id}/Documents/{type}")]
        public IActionResult PutDocument(int id, DocumentType type, ClientDocumentCreationData data)
        {
            return _ws.Perform(() =>
            {
                new ClientService(_dbContext).UpdateDocument(1, id, type, data);
            });
        }

        /// <summary>
        /// Unassign a document from a client
        /// </summary>
        /// <param name="id">Client id</param>
        /// <param name="type">Document type</param>
        /// <returns></returns>
        [HttpDelete("{id}/Documents/{type}")]
        public IActionResult PostDocument(int id, DocumentType type)
        {
            return _ws.Perform(() =>
            {
                new ClientService(_dbContext).RemoveDocument(1, id, type);
            });
        }
    }
}