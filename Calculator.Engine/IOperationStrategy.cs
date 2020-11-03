using Calculator.Infrastructure;

namespace Calculator.Engine
{
    public interface IOperationStrategy
    {
        decimal Execute(Operation operation);
    }
}
