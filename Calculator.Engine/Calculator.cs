using System.Threading.Tasks;

using Calculator.Infrastructure;

namespace Calculator.Engine
{
    public interface IDelayMachine
    {
        Task<int> DoSth(int input);
    }

    public class DelayMachine : IDelayMachine
    {
        public async Task<int> DoSth(int input)
        {
            await Task.Delay(5000).ConfigureAwait(true);
            return input * 100;
        }
    }

    public class Calculator : ICalculate
    {
        private readonly IOperationStrategyFactory _operationStrategyFactory;
        private readonly IDelayMachine delayMachine;

        public Calculator(IOperationStrategyFactory operationStrategyFactory, IDelayMachine delayMachine)
        {
            _operationStrategyFactory = operationStrategyFactory;
            this.delayMachine = delayMachine;
        }

        public decimal Execute(Operation operation)
        {
            var operationHandler = _operationStrategyFactory.Create(operation.Operator);
            decimal result;
            try
            {
                var xr = delayMachine.DoSth(20).GetAwaiter().GetResult();
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
