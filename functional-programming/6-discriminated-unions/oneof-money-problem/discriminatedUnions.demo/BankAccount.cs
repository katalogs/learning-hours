namespace discriminatedUnions.demo
{

    public class BankAccount
    {
        public string AccountNumber { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Balance Balance { get; }

        public BankAccount(string accountNumber, string firstName, string lastName, Balance balance)
        {
            AccountNumber = accountNumber;
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
        }
    }

}
