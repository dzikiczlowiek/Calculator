﻿using Calculator.Engine;
using Calculator.Infrastructure;

namespace Calculator.Operations
{
    public class AddOperationStrategy : IOperationStrategy
    {
        public decimal Execute(Operation operation)
        {
            return operation.Parameter1 + operation.Parameter2;
        }
    }
}
