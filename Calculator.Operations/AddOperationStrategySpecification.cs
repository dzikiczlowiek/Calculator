using Calculator.Engine;
using Calculator.Infrastructure;

namespace Calculator.Operations
{
    public class AddOperationStrategySpecification : OperationStrategySpecification<AddOperationStrategy>
    {
        public override bool IsApplicable(Operator @operator)
        {
            return @operator == Operator.Add;
        }
    }
}
