
using hitsLab.domain.exception;
using hitsLab.presentation.dto;

namespace hitsLab.presentation.middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext http)
        {
            try
            {
                await _next.Invoke(http);
            }
            catch (BaseException e)
            {
                http.Response.StatusCode = e.Status;
                await http.Response.WriteAsJsonAsync(e.Errors);
            }
            catch (Exception e)
            {
                http.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await http.Response.WriteAsJsonAsync(new Response("Error", e.Message));
            }
        }
    } 
}

