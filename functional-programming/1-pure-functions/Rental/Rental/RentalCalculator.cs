using System.Text;

namespace Rental
{
    public class RentalCalculator
    {
        private readonly IEnumerable<Rental> _rentals;
        public bool IsCalculated { get; private set; }
        public double Amount { get; private set; }

        public RentalCalculator(IEnumerable<Rental> rentals) => _rentals = rentals;

        public void Calculate()
        {
            if (!_rentals.Any())
            {
                throw new InvalidOperationException("No rentals on which perform calculation");
            }

            foreach (var rental in _rentals)
            {
                if (!IsCalculated)
                {
                    Amount += rental.Amount;
                }
            }
            
            IsCalculated = true;
        }

        private string FormatLine(Rental rental)
            => $"{rental.Date.ToString("dd-MM-yyyy")} : {rental.Label} | {rental.Amount}{Environment.NewLine}";

        public string DisplayRentals()
        {
            var result = new StringBuilder();

            foreach (var rental in _rentals)
            {
                result.Append(FormatLine(rental));
            }

            result.Append($"Total amount | {Amount}");
            return result.ToString();
        }
    }
}