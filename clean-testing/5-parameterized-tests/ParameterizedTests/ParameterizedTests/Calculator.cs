namespace ParameterizedTests
{
    public class Calculator
    {
        public const string Add = "add";
        public const string Multiply = "multiply";
        public const string Divide = "divide";
        public const string Subtract = "subtract";

        private static readonly Dictionary<string, Func<int, int, int>> SupportedOperators =
            new()
            {
                {Add, (a, b) => a + b},
                {Multiply, (a, b) => a * b},
                {Divide, (a, b) => a / b},
                {Subtract, (a, b) => a - b}
            };
    
        public int Calculate(int a, int b, string @operator) =>
            SupportedOperators.ContainsKey(@operator)
                ? SupportedOperators[@operator](a, b)
                : throw new ArgumentException("Not supported operator");
    }
}