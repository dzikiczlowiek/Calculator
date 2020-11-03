using System;

using Calculator.Infrastructure;

namespace Calculator.Engine
{
    public abstract class OperationStrategySpecification<TStrategy> : IOperationStrategySpecification
    {
        public Type StrategyType { get; } = typeof(TStrategy);

        public abstract bool IsApplicable(Operator @operator);
    }
}
