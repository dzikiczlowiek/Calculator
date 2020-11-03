using System;

using Calculator.Infrastructure;

namespace Calculator.Engine
{
    public interface IOperationStrategyFactory : IDisposable
    {
        IOperationStrategy Create(Operator @operator);

        void Release(IOperationStrategy operationStrategy);
    }
}
