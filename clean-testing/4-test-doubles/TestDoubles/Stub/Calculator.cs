using System;

namespace TestDoubles.Stub
{
    public class Calculator
    {
        private readonly IAuthorizer _authorizer;

        public Calculator(IAuthorizer authorizer)
        {
            _authorizer = authorizer;
        }

        public int Divide(int numerator, int denominator)
            => CheckAuthorization(DivideFunction(numerator, denominator));

        private int CheckAuthorization(Func<int> onSuccess) =>
            _authorizer.Authorize()
                ? onSuccess()
                : throw new UnauthorizedAccessException();

        private static Func<int> DivideFunction(int numerator, int denominator) =>
            () => denominator == 0
                ? throw new ArgumentException("Denominator can not be zero")
                : numerator / denominator;
    }
}