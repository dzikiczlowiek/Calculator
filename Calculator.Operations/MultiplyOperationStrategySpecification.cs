using Calculator.Engine;
using Calculator.Infrastructure;

namespace Calculator.Operations
{
    public class MultiplyOperationStrategySpecification : OperationStrategySpecification<MultiplyOperationStrategy>
    {
        public override bool IsApplicable(Operator @operator)
        {
            return @operator == Operator.Multiply;
        }
    }
}
