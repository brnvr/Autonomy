using AutonomyApi.WebService;
using AutonomyApi.Schedules;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Models.ViewModels.Schedule;
using AutonomyApi.Database;
using AutonomyApi.Models.Entities;

namespace AutonomyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : Controller
    {
        AutonomyDbContext _dbContext;
        WebServiceManager _ws;

        public ScheduleController(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
            _ws = new WebServiceManager(this);
        }

        /// <summary>
        /// List items from schedule
        /// </summary>
        /// <param name="search">Name filter (optional)</param>
        /// <param name="clientId">Client id (optional)</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery]ScheduleSearchView search)
        {
            return _ws.Perform(() =>
            {
                return Ok(new ScheduleService(_dbContext).Get(1, search));
            });
        }

        /// <summary>
        /// Find schedule item by id
        /// </summary>
        /// <param name="id">Schedule id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return _ws.Perform(() =>
            {
                return Ok(new ScheduleService(_dbContext).Get(1, id));
            });
        }

        /// <summary>
        /// Create item in schedule
        /// </summary>
        /// <param name="data">Schedule data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ScheduleCreationView data)
        {
            return _ws.Perform(() =>
            {
                var id = new ScheduleService(_dbContext).Create(1, data);

                return CreatedAtAction(nameof(Get), new { id }, new { id });
            });
        }

        /// <summary>
        /// Update item from schedule
        /// </summary>
        /// <param name="id">Schedule id</param>
        /// <param name="data">Schedule data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ScheduleUpdateView data)
        {
            return _ws.Perform(() =>
            {
                new ScheduleService(_dbContext).Update(1, id, data);
            });
        }

        /// <summary>
        /// Remove item from schedule
        /// </summary>
        /// <param name="id">Schedule id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _ws.Perform(() =>
            {
                new ScheduleService(_dbContext).Remove(1, id);
            });
        }

        /// <summary>
        /// Add client to schedule item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpPost("{id}/Client/{clientId}")]
        public IActionResult AddClient(int id, int clientId)
        {
            return _ws.Perform(() =>
            {
                new ScheduleService(_dbContext).AddClient(1, id, clientId);
            });
        }

        /// <summary>
        /// Remove client from schedule item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpDelete("{id}/Client/{clientId}")]
        public IActionResult RemoveClient(int id, int clientId)
        {
            return _ws.Perform(() =>
            {
                new ScheduleService(_dbContext).RemoveClient(1, id, clientId);
            });
        }
    }
}