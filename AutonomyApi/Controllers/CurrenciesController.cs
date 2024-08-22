/*using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Models.ViewModels.Client;
using AutonomyApi.Enums;
using AutonomyApi.Database;

namespace AutonomyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrenciesController : Controller
    {
        AutonomyDbContext _dbContext;
        WebServiceManager _ws;

        public CurrenciesController(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
            _ws = new WebServiceManager(this);
        }

        /// <summary>
        /// List all currencies
        /// </summary>
        /// <param name="search">Filter by code or name</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string? search)
        {
            return _ws.Perform(() =>
            {
                return Ok(new CurrencyService(_dbContext).Get(search));
            });
        }

        /// <summary>
        /// Find currency by id
        /// </summary>
        /// <param name="id">Currency id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return _ws.Perform(() =>
            {
                return Ok(new CurrencyService(_dbContext).Get(id));
            });
        }

    }
}*/