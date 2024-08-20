using AutonomyApi.WebService;
using AutonomyApi.Services;
using Microsoft.AspNetCore.Mvc;
using AutonomyApi.Database;
using AutonomyApi.Models.Entities;

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

    }
}