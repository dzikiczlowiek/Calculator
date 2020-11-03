using System;

using Calculator.Infrastructure;

namespace Calculator.Engine
{
    public interface IOperationStrategySpecification
    {
        Type StrategyType { get; }

        bool IsApplicable(Operator @operator);
    }
}
