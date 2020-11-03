using Calculator.Infrastructure;

namespace Calculator.Engine
{
    public class Calculator : ICalculate
    {
        private readonly IOperationStrategyFactory _operationStrategyFactory;

        public Calculator(IOperationStrategyFactory operationStrategyFactory)
        {
            _operationStrategyFactory = operationStrategyFactory;
        }

        public decimal Execute(Operation operation)
        {
            var operationHandler = _operationStrategyFactory.Create(operation.Operator);
            decimal result;
            try
            {
                result = operationHandler.Execute(operation);
            }
            finally
            {
                _operationStrategyFactory.Release(operationHandler);
            }

            return result;
        }
    }
}
