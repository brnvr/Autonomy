using AutonomyApi.Database;
using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Models.ViewModels.Budget;
using Microsoft.AspNetCore.JsonPatch;
using System.ComponentModel.DataAnnotations;
using AutonomyApi.Models.Entities;

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
        /// Search budgets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] BudgetSearchView search)
        {
            return _ws.Perform(() =>
            {
                return Ok(new BudgetService(_dbContext).Get(1, search));
            });
        }

        /// <summary>
        /// Find budget by id
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
        /// Create budget
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(BudgetCreationView data)
        {
            return _ws.Perform(() =>
            {
                var id = new BudgetService(_dbContext).Create(1, data);

                return CreatedAtAction(nameof(Get), new { id }, new { id });
            });
        }

        /// <summary>
        /// Copy budget
        /// </summary>
        /// <param name="id">Source budget id</param>
        /// <param name="name">Name of the new budget</param>
        /// <returns></returns>
        [HttpPost("Copy")]
        public IActionResult Copy([Required]int id, [Required]string name)
        {
            return _ws.Perform(() =>
            {
                var newId = new BudgetService(_dbContext).Copy(1, id, name);

                return CreatedAtAction(nameof(Get), new { id = newId }, new { id = newId });
            });
        }

        /// <summary>
        /// Update budget
        /// </summary>
        /// <param name="id">Budget id</param>
        /// <param name="data">Data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, BudgetUpdateView data)
        {
            return _ws.Perform(() =>
            {
                new BudgetService(_dbContext).Update(1, id, data);
            });
        }

        /// <summary>
        /// Remove budget
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

        /// <summary>
        /// Update items from budget
        /// </summary>
        /// <param name="id">Budget id</param>
        /// <param name="data">Items</param>
        /// <returns></returns>
        [HttpPut("{id}/Items")]
        public IActionResult PutItems(int id, BudgetItemUpdateViewList data)
        {
            return _ws.Perform(() =>
            {
                new BudgetService(_dbContext).UpdateItems(1, id, data);
            });
        }
    }
}