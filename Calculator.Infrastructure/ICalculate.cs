namespace Calculator.Infrastructure
{
    public interface ICalculate
    {
        decimal Execute(Operation operation);
    }
}
