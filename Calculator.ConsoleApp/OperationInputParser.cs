using System;
using System.Text.RegularExpressions;

using Calculator.Infrastructure;

namespace Calculator.ConsoleApp
{
    public class OperationInputParser : IOperationParser
    {
        private readonly Regex regex = new Regex(@"^(?<p1>\d+)\W*(?<operator>[\+\-\*\/])\W*(?<p2>\d+)", RegexOptions.Compiled);

        public Operation Parse(string input)
        {
            var match = regex.Match(input);
            var p1 = match.Groups["p1"].Value;
            var op = match.Groups["operator"].Value;
            var p2 = match.Groups["p2"].Value;

            var operation = new Operation();
            operation.Parameter1 = decimal.Parse(p1);
            operation.Operator = ParseOperator(op);
            operation.Parameter2 = decimal.Parse(p2);

            return operation;
        }

        private static Operator ParseOperator(string op)
        {
            switch (op)
            {
                case "+":
                    return Operator.Add;
                case "-":
                    return Operator.Subtract;
                case "*":
                    return Operator.Multiply;
                case "/":
                    return Operator.Divide;
                default:
                    throw new InvalidOperationException();

            }
        }
    }
}
