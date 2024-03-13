using LoggerMikroservis.Manager;
using LoggerMikroservis.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoggerMikroservis.Controllers
{
    [Route("api/logger")]
    [ApiController]
    public class LoggerController : Controller
    {
        private readonly ILoggerManager _logger;

        public LoggerController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult PostLogMessage([FromBody] Log log)
        { 
            try
            {
                string message = log.ServiceName + " ---> " + log.Method + " ---> " + log.Message;

                if(log.LogLevel == LogLevel.Information)
                {
                    _logger.LogInfo(message);
                }
                else if(log.LogLevel == LogLevel.Error)
                {
                    _logger.LogError(log.Exception, message);
                }
                else if(log.LogLevel == LogLevel.Debug)
                {
                    _logger.LogDebug(message);
                }
                else if(log.LogLevel == LogLevel.Warning)
                {
                    _logger.LogWarn(message);
                }

                return Ok(Task.FromResult("Message was successfully created."));
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error during creation of log file.");
                return StatusCode(500, "Error during creation of log file");
            }
        }
    }
}
