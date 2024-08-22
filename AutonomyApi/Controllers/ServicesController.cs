using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Models.ViewModels.Service;
using AutonomyApi.Database;
using System.ComponentModel.DataAnnotations;

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
        /// Search services
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] ServiceSearchView search)
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
        public IActionResult Post([Required] ServiceCreationView data)
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
        public IActionResult Put(int id, [Required] ServiceUpdateView data)
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
        /// Get service's budget template
        /// </summary>
        /// <param name="id">Service id</param>
        /// <returns></returns>
        [HttpGet("{id}/BudgetTemplate")]
        public IActionResult GetBudgetTemplate(int id)
        {
            return _ws.Perform(() =>
            {
                return Ok(new ServiceService(_dbContext).GetBudgetTemplate(1, id));
            });
        }

        /// <summary>
        /// Update service's budget template
        /// </summary>
        /// <param name="id">Service id</param>
        /// <param name="data">Document</param>
        /// <returns></returns>
        [HttpPut("{id}/BudgetTemplate")]
        public IActionResult PutBudgetTemplate(int id, [Required] BudgetTemplateUpdateView data)
        {
            return _ws.Perform(() =>
            {
                new ServiceService(_dbContext).UpdateBudgetTemplate(1, id, data);
            });
        }

        /// <summary>
        /// Remove service's budget template
        /// </summary>
        /// <param name="id">Service id</param>
        /// <returns></returns>
        [HttpDelete("{id}/BudgetTemplate")]
        public IActionResult DeleteBudgetTemplate(int id)
        {
            return _ws.Perform(() =>
            {
                new ServiceService(_dbContext).RemoveBudgetTemplate(1, id);
            });
        }

        /// <summary>
        /// Provide a service to a client or a list of clients
        /// </summary>
        /// <param name="id">Service id</param>
        /// <param name="data">Service provided data</param>
        /// <returns></returns>
        [HttpPost("{id}/Provide")]
        public IActionResult Provide(int id, [Required] ServiceProvideView data)
        {
            return _ws.Perform(() =>
            {
                var serviceProvidedId = new ServiceService(_dbContext).Provide(1, id, data);

                return CreatedAtAction("Get", "ServicesProvided", new { id = serviceProvidedId }, new { serviceProvidedId });
            });
        }
    }
}