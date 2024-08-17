using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Models.ViewModels.Service;
using AutonomyApi.Enums;
using AutonomyApi.Database;

namespace AutonomyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : Controller
    {
        AutonomyDbContext _dbContext;
        WebServiceManager _ws;

        public ServicesController(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
            _ws = new WebServiceManager(this);
        }

        /// <summary>
        /// List all services
        /// </summary>
        /// <param name="search">Name filter (optional)</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string? search)
        {
            return _ws.Perform(() =>
            {
                return Ok(new ServiceService(_dbContext).Get(1, search));
            });
        }

        /// <summary>
        /// Find service by id
        /// </summary>
        /// <param name="id">Service id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return _ws.Perform(() =>
            {
                return Ok(new ServiceService(_dbContext).Get(1, id));
            });
        }

        /// <summary>
        /// Register new service
        /// </summary>
        /// <param name="data">Service data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ServiceCreationView data)
        {
            return _ws.Perform(() =>
            {
                var id = new ServiceService(_dbContext).Create(1, data);

                return CreatedAtAction(nameof(Get), new { id }, new { id });
            });
        }

        /// <summary>
        /// Update service
        /// </summary>
        /// <param name="id">Service id</param>
        /// <param name="data">Service data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ServiceUpdateView data)
        {
            return _ws.Perform(() =>
            {
                new ServiceService(_dbContext).Update(1, id, data);
            });
        }

        /// <summary>
        /// Remove service
        /// </summary>
        /// <param name="id">Service id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _ws.Perform(() =>
            {
                new ServiceService(_dbContext).Remove(1, id);
            });
        }

        /// <summary>
        /// Update service's budget template
        /// </summary>
        /// <param name="id">Service id</param>
        /// <param name="data">Document</param>
        /// <returns></returns>
        [HttpPut("{id}/BudgetTemplate")]
        public IActionResult PutDocument(int id, BudgetTemplateUpdateView? data)
        {
            return _ws.Perform(() =>
            {
                new ServiceService(_dbContext).UpdateBudgetTemplate(1, id, data);
            });
        }
    }
}