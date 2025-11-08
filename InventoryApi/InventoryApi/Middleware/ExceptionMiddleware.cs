using System.Text.Json;

namespace InventoryApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex) 
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    InvalidOperationException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                var result = JsonSerializer.Serialize(
                    new
                    {
                        error = ex.Message
                    });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
