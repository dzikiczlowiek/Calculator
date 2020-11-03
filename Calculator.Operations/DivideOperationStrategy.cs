using System;

using Calculator.Engine;
using Calculator.Infrastructure;

namespace Calculator.Operations
{
    public class DivideOperationStrategy : IOperationStrategy
    {
        public decimal Execute(Operation operation)
        {
            if (operation.Parameter2 == 0)
            {
                throw new InvalidOperationException();
            }

            if (operation.Parameter1 == 0)
            {
                return 0m;
            }

            return operation.Parameter1 / operation.Parameter2;
        }
    }
}
