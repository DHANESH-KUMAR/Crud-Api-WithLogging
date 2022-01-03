using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
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
        public async void OnActionExecuting(ActionExecutingContext context)
        {
            await LogRequest(context.HttpContext);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            RecyclableMemoryStreamManager _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}");
            context.Request.Body.Position = 0;
        }

        public async void OnActionExecuted(ActionExecutedContext context)
        {
          //  await LogResponse(context.HttpContext);

        }

        private async Task LogResponse(HttpContext context)
        {
            RecyclableMemoryStreamManager _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text}");
            await responseBody.CopyToAsync(originalBodyStream);
        }
        private string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }

    }
}
