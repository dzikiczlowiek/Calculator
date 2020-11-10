
using Calculator.Engine;
using Calculator.Infrastructure;

namespace Calculator.Operations
{
    public class MultiplyOperationStrategy : IOperationStrategy
    {
        public decimal Execute(Operation operation)
        {
            if (operation.Parameter1 == 0 || operation.Parameter2 == 0)
            {
                return 0m;
            }

            return operation.Parameter1 * operation.Parameter2;
        }
    }
}
