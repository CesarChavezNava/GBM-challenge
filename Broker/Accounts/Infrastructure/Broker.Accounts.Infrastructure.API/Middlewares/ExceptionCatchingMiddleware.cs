using Broker.Accounts.Domain.Exceptions;
using Broker.Core.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Broker.Accounts.Infrastructure.API.Middlewares;

public class ExceptionCatchingMiddleware
{
    private const string INTERNAL_SERVER_ERROR = @"Internal Server Error. The server encountered an unexpected condition that prevented it from fulfilling the request. We apologize for the inconvenience and are working to resolve the issue as soon as possible. Please try again later.";

    private readonly RequestDelegate next;
    public ExceptionCatchingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        } 
        catch (Exception exception)
        {
            await HandleException(context, exception);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode = exception switch
        {
            ArgumentNullOrEmptyException or
            InsufficientCashException or
            InsufficientSharesException or
            InvalidOrderOperationException or
            InvalidStockSymbolException or
            InvalidSearchOrderCodeException or
            InvalidUserIdException or
            MinutesOutOfRangeException or
            SearchLimitOutOfRangeException or
            SearchOffsetOutOfRangeException or
            TimestampOutOfRangeException or
            TooLowSharePriceException => HttpStatusCode.BadRequest,
            UserNotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };

        string message = exception.Message;
        if(statusCode == HttpStatusCode.InternalServerError)
            message = INTERNAL_SERVER_ERROR;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        string[] errors = new string[] { message };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
        {
            errors
        }));
    }
}
