using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace school.app.Filters
{
    public class RequestResponseLoggerAttribute : Attribute, IActionFilter
    {
        private readonly ILogger<RequestResponseLoggerAttribute> _logger;
        public RequestResponseLoggerAttribute(ILogger<RequestResponseLoggerAttribute> logger)
        {
            this._logger = logger;
        }
        public  void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var jsonString = JsonConvert.SerializeObject(context.Result);

            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                    $"Schema:{request.Scheme} " +
                                    $"Host: {request.Host} " +
                                    $"Path: {request.Path} " +
                                    $"QueryString: {request.QueryString} " +
                                    $"Request Body: {jsonString}");
        }

        

        public  void OnActionExecuted(ActionExecutedContext context)
        {
            var request = context.HttpContext.Request;
            var jsonString = JsonConvert.SerializeObject(context.Result);

            _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                    $"Schema:{request.Scheme} " +
                                    $"Host: {request.Host} " +
                                    $"Path: {request.Path} " +
                                    $"QueryString: {request.QueryString} " +
                                    $"Request Body: {jsonString}");

        }

      

    }
}
