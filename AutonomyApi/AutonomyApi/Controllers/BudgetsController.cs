using AutonomyApi.Database;
using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Models.Dtos;
using AutonomyApi.Models.Dtos.Client;
using AutonomyApi.Models.Views.Client;
using AutonomyApi.Models.Entities;
using AutonomyApi.Enums;
using AutonomyApi.Models.Views.Budget;

namespace AutonomyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetsController : Controller
    {
        AutonomyDbContext _dbContext;
        WebServiceManager _ws;

        public BudgetsController(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
            _ws = new WebServiceManager(this);
        }

        /// <summary>
        /// List all budgets
        /// </summary>
        /// <param name="search">Name filter (optional)</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string? search)
        {
            return _ws.Perform(() =>
            {
                return Ok(new BudgetService(_dbContext).Get(1, search));
            });
        }

        /// <summary>
        /// Find a budget by id
        /// </summary>
        /// <param name="id">Budget id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return _ws.Perform(() =>
            {
                return Ok(new BudgetService(_dbContext).Get(1, id));
            });
        }

        /// <summary>
        /// Create a budget
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(BudgetCreationData data)
        {
            return _ws.Perform(() =>
            {
                var id = new BudgetService(_dbContext).Add(1, data);

                return CreatedAtAction(nameof(Get), new { id }, new { id });
            });
        }

        /// <summary>
        /// Update a budget
        /// </summary>
        /// <param name="id">Budget id</param>
        /// <param name="data">Data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put( int id, BudgetUpdateData data)
        {
            return _ws.Perform(() =>
            {
                new BudgetService(_dbContext).Update(1, id, data);
            });
        }

        /// <summary>
        /// Remove a budget
        /// </summary>
        /// <param name="id">Budget id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _ws.Perform(() =>
            {
                new BudgetService(_dbContext).Remove(1, id);
            });
        }
    }
}