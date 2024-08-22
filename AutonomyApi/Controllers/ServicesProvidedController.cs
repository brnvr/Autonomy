using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.ServiceProvided;

namespace AutonomyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesProvidedController : Controller
    {
        AutonomyDbContext _dbContext;
        WebServiceManager _ws;

        public ServicesProvidedController(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
            _ws = new WebServiceManager(this);
        }

        /// <summary>
        /// Search services provided
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] ServiceProvidedSearchView search)
        {
            return _ws.Perform(() =>
            {
                return Ok(new ServiceProvidedService(_dbContext).Get(1, search));
            });
        }
    }
}