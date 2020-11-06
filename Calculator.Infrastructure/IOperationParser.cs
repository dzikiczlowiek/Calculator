namespace Calculator.Infrastructure
{
    public interface IOperationParser
    {
        Operation Parse(string input);
    }
}
