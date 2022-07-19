namespace discriminatedUnions.demo
{

    public record Balance
    {
        private readonly int value;

        private Balance(int balance)
            => value = balance;

        public static Balance From(int balance)
            => (balance <= 0) ?
                throw new ArgumentException("Balance must be positive") :
                new Balance(balance);
    }

}
