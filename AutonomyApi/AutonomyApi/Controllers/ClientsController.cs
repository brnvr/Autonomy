using AutonomyApi.Database;
using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Dtos;

namespace AutonomyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        AppDbContext _dbContext;
        WebServiceManager _ws;

        public ClientsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _ws = new WebServiceManager(this);
        }

        /// <summary>
        /// Lists clients
        /// </summary>
        /// <param name="name">Filters clients by name (optional)</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string? name)
        {
            return _ws.Perform(() =>
            {
                return Ok(new ClientService(_dbContext).Get(1, name));
            });
        }

        /// <summary>
        /// Gets a client by id
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
        /// Registers a new client
        /// </summary>
        /// <param name="data">Client data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(InsertClientDto data)
        {
            return _ws.Perform(() =>
            {
                var id = new ClientService(_dbContext).Register(1, data);

                return CreatedAtAction(nameof(Get), new { id }, new { id });
            });
        }

        /// <summary>
        /// Edits a client
        /// </summary>
        /// <param name="id">Client id</param>
        /// <param name="data">Client data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Edit(int id, UpdateClientDto data)
        {
            return _ws.Perform(() =>
            {
                new ClientService(_dbContext).Edit(1, id, data);
            });
        }

        /// <summary>
        /// Remove a client
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            return _ws.Perform(() =>
            {
                new ClientService(_dbContext).Remove(1, id);
            });
        }
    }
}