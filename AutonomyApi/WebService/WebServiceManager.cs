using AutonomyApi.WebService.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;
using System.Security.Authentication;

namespace AutonomyApi.WebService
{
    public class WebServiceManager
    {
        protected ControllerBase Controller { get; set; }

        public WebServiceManager(ControllerBase controller)
        {
            Controller = controller;
        }

        public IActionResult Perform(Func<IActionResult> callback)
        {
            if (!Controller.ModelState.IsValid)
            {
                return Controller.BadRequest(Controller.ModelState);
            }

            try
            {
                return callback();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        public IActionResult Perform(Action callback)
        {
            if (!Controller.ModelState.IsValid)
            {
                return Controller.BadRequest(Controller.ModelState);
            }

            try
            {
                callback();

                return Controller.NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        public async Task<IActionResult> PerformAsync(Func<Task<IActionResult>> callback)
        {
            if (!Controller.ModelState.IsValid)
            {
                return Controller.BadRequest(Controller.ModelState);
            }

            try
            {
                return await callback();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        public async Task<IActionResult> PerformAsync(Func<Task> callback)
        {
            if (!Controller.ModelState.IsValid)
            {
                return Controller.BadRequest(Controller.ModelState);
            }

            try
            {
                await callback();

                return Controller.NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        protected dynamic GetError(HttpStatusCode status, params string[] errors)
        {
            return new
            {
                errors, 
                status = (int)status,
                type = "",
                title = "One or more request errors occurred.",
                traceId = Controller.HttpContext.TraceIdentifier,
            };
        }

        protected IActionResult HandleException(Exception ex)
        {
            if (ex is DbUpdateException dbEx)
            {
                var _dbEx = ex.InnerException as DbException;

                if (_dbEx != null)
                {
                    if (_dbEx.ErrorCode == -2147467259)
                    {
                        return Controller.Conflict(GetError(HttpStatusCode.Conflict, _dbEx.Message));
                    }
                }

                return Controller.UnprocessableEntity(GetError(HttpStatusCode.UnprocessableEntity, (dbEx.InnerException ?? dbEx).Message));
            }
            else if (ex is HttpRequestException httpEx)
            {
                return Controller.StatusCode((int)(httpEx.StatusCode ?? HttpStatusCode.BadRequest), GetError(HttpStatusCode.BadRequest, httpEx.Message));
            }
            else if (ex is AuthenticationException authEx)
            {
                return Controller.Unauthorized(GetError(HttpStatusCode.Unauthorized, authEx.Message));
            }
            else if (ex is EntityNotFoundException entryEx)
            {
                return Controller.NotFound(GetError(HttpStatusCode.NotFound, entryEx.Message));
            }
            else if (ex is DuplicateValueException dupEx)
            {
                return Controller.Conflict(GetError(HttpStatusCode.Conflict, dupEx.Message));
            }
            else
            {
                return Controller.BadRequest(GetError(HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }

    public class WebServiceManager<TContext> : WebServiceManager where TContext : DbContext
    {
        TContext _dbContext { get; set; }

        public WebServiceManager(ControllerBase controller, TContext dbContext) : base(controller)
        {
            _dbContext = dbContext;
        }

        public IActionResult Perform(Func<TContext, IActionResult> callback)
        {
            if (!Controller.ModelState.IsValid)
            {
                return Controller.BadRequest(Controller.ModelState);
            }


            try
            {
                return callback(_dbContext);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        public IActionResult Perform(Action<TContext> callback)
        {
            if (!Controller.ModelState.IsValid)
            {
                return Controller.BadRequest(Controller.ModelState);
            }


            try
            {
                callback(_dbContext);

                return Controller.NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        public async Task<IActionResult> PerformAsync(Func<TContext, Task<IActionResult>> callback)
        {
            if (!Controller.ModelState.IsValid)
            {
                return Controller.BadRequest(Controller.ModelState);
            }


            try
            {
                return await callback(_dbContext);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        public async Task<IActionResult> PerformAsync(Func<TContext, Task> callback)
        {
            if (!Controller.ModelState.IsValid)
            {
                return Controller.BadRequest(Controller.ModelState);
            }


            try
            {
                await callback(_dbContext);

                return Controller.NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }
    }
}
