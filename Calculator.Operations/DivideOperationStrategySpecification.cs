using Calculator.Engine;
using Calculator.Infrastructure;

namespace Calculator.Operations
{
    public class DivideOperationStrategySpecification : OperationStrategySpecification<DivideOperationStrategy>
    {
        public override bool IsApplicable(Operator @operator)
        {
            return @operator == Operator.Divide;
        }
    }
}
