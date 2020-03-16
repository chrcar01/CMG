using System.IO;
using System.Threading.Tasks;
using CMG.Tools.Evaluators;
using Microsoft.AspNetCore.Mvc;

namespace CMG.SensorWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EvaluatorController : ControllerBase
    {
        [HttpGet]
        public string Hello()
        {
            return "Hello! Post the contents of your log file to http://localhost:51505/evaluator";
        }

        [HttpPost]
        public async Task<ActionResult<string>> Evaluate()
        {
            var logFile = await new StreamReader(Request.Body).ReadToEndAsync();
            return Ok(SensorEvaluator.EvaluateLogFile(logFile));
        }
    }
}
