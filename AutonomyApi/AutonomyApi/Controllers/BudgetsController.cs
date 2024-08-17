﻿using AutonomyApi.Database;
using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Models.ViewModels.Budget;
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpPatch("[Action]")]
        public IActionResult PatchTest(JsonPatchDocument<BudgetCreationView> lol)
        {
            return _ws.Perform(() =>
            {
                return Ok(lol);
            });
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
                var id = new BudgetService(_dbContext).Add(1, data);

                return CreatedAtAction(nameof(Get), new { id }, new { id });
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