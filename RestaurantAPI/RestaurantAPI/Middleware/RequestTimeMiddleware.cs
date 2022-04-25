using System.Diagnostics;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private  ILogger<RequestTimeMiddleware> logger;
        private Stopwatch _stopWatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _stopWatch = new Stopwatch();
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
           await  next.Invoke(context);
            _stopWatch.Stop();

           var elapsedMiliseconds =  _stopWatch.ElapsedMilliseconds;
            if (elapsedMiliseconds/1000 >4)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path } took {elapsedMiliseconds} ms";
                logger.LogInformation(message); 
            }
        }
    }
}
