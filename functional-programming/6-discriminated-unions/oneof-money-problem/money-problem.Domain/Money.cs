namespace money_problem.Domain
{
    public record Money(double Amount, Currency Currency)
    {
        public Money Times(int multiplier) => this with {Amount = Amount * multiplier};

        public Money Divide(int divisor) => this with {Amount = Amount / divisor};
        public override string ToString() => $"{Amount} {Currency}";
    }
}