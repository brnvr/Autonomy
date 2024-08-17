using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

public class ExceptionHandlingEndpointFilter : IEndpointFilter
{

    public ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            // Proceed to the endpoint
            return next(context);
        }
        catch (FormatException ex)
        {
            // Handle FormatException and return a custom response
            return next(context);
        }
    }
}