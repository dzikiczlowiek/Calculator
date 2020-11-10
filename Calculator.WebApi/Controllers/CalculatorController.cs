using System.Threading.Tasks;

using Calculator.Infrastructure;
using Calculator.Infrastructure.Cache;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculate calculate;
        private readonly IOperationParser operationParser;
        private readonly ICacheProvider cacheProvider;

        public CalculatorController(
            ICalculate calculate, 
            IOperationParser operationParser, 
            ICacheProvider cacheProvider)
        {
            this.calculate = calculate;
            this.operationParser = operationParser;
            this.cacheProvider = cacheProvider;
        }

        [HttpGet("{parameter1}/{operator}/{parameter2}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Calculate(decimal parameter1, Operator @operator, decimal parameter2)
        {
            var operation = new Operation
            {
                Operator = @operator,
                Parameter1 = parameter1,
                Parameter2 = parameter2
            };

            var result = calculate.Execute(operation);

            return Ok(result);

            //var response = await _requestDispatcher.Send(new CreateTodoItemRequest { Name = item.Name, Content = item.Content });
            //return CreatedAtAction(nameof(GetTodoItem), new { id = response.Payload.Id }, Map(response.Payload));
            //TodoItemModel Map(TodoItem source)
            //{
            //    var model = new TodoItemModel();
            //    model.Id = source.Id;
            //    model.IsComplete = source.IsCompleted;
            //    model.Name = source.Name;
            //    model.Content = source.Content;
            //    return model;
            //}
        }
    }
}
