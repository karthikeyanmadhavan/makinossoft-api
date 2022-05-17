using Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JSONFileAPI.Common
{
    public class ApiExceptionHandler
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiExceptionHandler> _logger;
        /// <summary>
        /// Handler - constructor
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        /// <param name="_loggerFactory"></param>
        public ApiExceptionHandler(IWebHostEnvironment environment, IConfiguration configuration, ILoggerFactory _loggerFactory)
        {
            _logger = _loggerFactory.CreateLogger<ApiExceptionHandler>();
            _env = environment;
            _configuration = configuration;
        }
        /// <summary>
        /// Invoke method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                if (ex == null) return;

                if (!context.Request.Path.StartsWithSegments(new PathString("/api")))
                {
                    return;
                }

                if (!(ex is ApiException) && !(ex is UnauthorizedAccessException))
                {
                    var env = $"{_configuration.GetSection("ApplicationName").Value} - {_env.EnvironmentName}";
                    await WriteDetailedLog(context.Request, ex, env);
                }

                ApiResponse<object> res = null;
                switch (ex)
                {
                    case ApiException exception:
                        string customMessage = MemCache.GetApiMessage(exception.Code);
                        res = new ApiResponse<object>
                        {
                            Code = exception.Code,
                            Message = String.IsNullOrEmpty(customMessage) ? exception.Message : customMessage,
                        };                      
                        break;
                    default:
                        res = new ApiResponse
                        {
                            Message = ex.Message
                        };
                        if (_env.EnvironmentName != "Development")
                        {
                            res.Message = MemCache.GetApiMessage(0);
                        }
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                context.Response.Headers.Add("Content-Type", "application/json");
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(res, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task WriteDetailedLog(HttpRequest request, Exception ex, string projectEnvironment)
        {
            var body = string.Empty;
            using (var stream = request.Body)
            {
                var reader = new StreamReader(stream, Encoding.UTF8);
                body = await reader.ReadToEndAsync();
            }

            _logger.LogError(ex, JsonConvert.SerializeObject(new
            {
                EnvironmentName = projectEnvironment,
                RequestDateTime = DateTimeOffset.UtcNow,
                Url = request.Path,
                Error = ex.Message,
                Protocol = request.Protocol,
                Host = request.Host,
                Cookies = request.Cookies,
                QueryString = request.Query,
                Body = body,
                Headers = request.Headers,
                IPAddress = request.HttpContext.Connection.RemoteIpAddress.ToString(),
                RemotePort = request.HttpContext.Connection.RemotePort,
                Method = request.Method,
                LocalIpAddress = request.HttpContext.Connection.LocalIpAddress.ToString(),
                LocalPort = request.HttpContext.Connection.LocalPort,
                Referer = request.Headers["Referer"].ToString(),
                IsHttps = request.IsHttps
            }));

        }
    }
}
