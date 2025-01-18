using System.Net;
using System.Text.Json;
using Workflow.API.Core.Exceptions;

namespace Workflow.API.Infrastructure.Web.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private RequestDelegate _next;
        private IWebHostEnvironment _env;


        public GlobalExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message, ex.Errors);
            }
            catch (TokenParseException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message, new List<string> { ex.Message });
            }
            catch (UserNotAuthenticatedException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Unauthorized, ex.Message, (object?)null);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Error occured while processing request", 
                       (_env.IsDevelopment() ? ex.ToString() : (object?)null));
            }
        }

        private static Task HandleExceptionAsync<T>(HttpContext context, HttpStatusCode statusCode, string message, T data)
        {
            // Format the response
            var response = context.Response;
            response.ContentType = "application/json";

            // You can customize the status code based on the exception type
            response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                IsSuccess = false,
                Message = message,
                Data = data
            };

            var errorJson = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            return response.WriteAsync(errorJson);
        }
    }
}
