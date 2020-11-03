using Calculator.Engine;
using Calculator.Infrastructure;

namespace Calculator.Operations
{
    public class SubtractOperationStrategySpecification : OperationStrategySpecification<SubtractOperationStrategy>
    {
        public override bool IsApplicable(Operator @operator)
        {
            return @operator == Operator.Subtract;
        }
    }
}
