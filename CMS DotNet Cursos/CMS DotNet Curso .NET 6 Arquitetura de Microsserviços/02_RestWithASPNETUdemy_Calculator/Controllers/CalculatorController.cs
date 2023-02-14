using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNETUdemy_Calculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
       
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            _logger.LogInformation("sum");

            if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
                return BadRequest("Invalid Input");

            var resultado = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);

            return Ok(resultado.ToString());
        }

        [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(string firstNumber, string secondNumber)
        {
            _logger.LogInformation("subtraction");

            if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
                return BadRequest("Invalid Input");

            var resultado = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);

            return Ok(resultado.ToString());
        }

        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(string firstNumber, string secondNumber)
        {
            _logger.LogInformation("multiplication");

            if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
                return BadRequest("Invalid Input");

            var resultado = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);

            return Ok(resultado.ToString());
        }

        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {
            _logger.LogInformation("division");

            if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
                return BadRequest("Invalid Input");

            var resultado = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);

            return Ok(resultado.ToString());
        }

        [HttpGet("mean/{firstNumber}/{secondNumber}")]
        public IActionResult Mean(string firstNumber, string secondNumber)
        {
            _logger.LogInformation("mean");

            if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
                return BadRequest("Invalid Input");

            var resultado = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;

            return Ok(resultado.ToString());
        }

        [HttpGet("square-root/{firstNumber}")]
        public IActionResult SquareRoot(string firstNumber)
        {
            _logger.LogInformation("square-root");

            if (!IsNumeric(firstNumber))
                return BadRequest("Invalid Input");

            var resultado = Math.Sqrt((double)ConvertToDecimal(firstNumber));

            return Ok(resultado.ToString());
        }

        private bool IsNumeric(string strNumber)
        {
            double number;

            bool isNumber = double.TryParse(
                strNumber, 
                System.Globalization.NumberStyles.Any, 
                System.Globalization.NumberFormatInfo.InvariantInfo, 
                out number);

            return isNumber;
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;

            if (decimal.TryParse(strNumber, out decimalValue))
                return decimalValue;

            return 0;
        }
    }
}