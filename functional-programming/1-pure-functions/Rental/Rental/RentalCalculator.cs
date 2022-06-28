using System.Text;

namespace Rental
{
    public class RentalCalculator
    {
        private readonly IEnumerable<Rental> _rentals;
        public bool IsCalculated { get; private set; }
        public double Amount { get; private set; }

        public RentalCalculator(IEnumerable<Rental> rentals) => _rentals = rentals;

        public string Calculate()
        {
            if (!_rentals.Any())
            {
                throw new InvalidOperationException("No rentals on which perform calculation");
            }

            var result = new StringBuilder();

            foreach (var rental in _rentals)
            {
                if (!IsCalculated)
                {
                    Amount += rental.Amount;
                }

                result.Append(FormatLine(rental, rental.Amount));
            }

            result.Append($"Total amount | {Amount}");
            IsCalculated = true;

            return result.ToString();
        }

        private string FormatLine(Rental rental, double amount)
            => $"{rental.Date.ToString("dd-MM-yyyy")} : {rental.Label} | {rental.Amount}{Environment.NewLine}";
    }
}