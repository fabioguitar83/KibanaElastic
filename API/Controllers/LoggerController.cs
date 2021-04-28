using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggerController : ControllerBase
    {

        private readonly ILogger<LoggerController> _logger;

        public LoggerController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("log-info")]
        public IActionResult LogInfo()
        {
            _logger.LogInformation("Log Criado");
            return Ok();
        }

        [HttpPost]
        [Route("log-error")]
        public IActionResult LogError()
        {
            _logger.LogError("Log Criado");
            return Ok();
        }

        [HttpPost]
        [Route("log-warning")]
        public IActionResult LogWarning()
        {
            _logger.LogWarning("Log Criado");
            return Ok();
        }

        [HttpPost]
        [Route("log-exception")]
        public IActionResult LogException()
        {
            throw new Exception("Erro exemplo");          
        }


    }
}
