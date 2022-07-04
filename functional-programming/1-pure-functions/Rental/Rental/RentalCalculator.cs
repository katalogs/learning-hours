using System.Text;

namespace Rental
{
    public class RentalCalculator
    {
        private readonly IEnumerable<Rental> _rentals;
        public double Amount { get; set; }

        public RentalCalculator(IEnumerable<Rental> rentals) => _rentals = rentals;

        public static double Calculate(IEnumerable<Rental> rentals)
        {
            var rentalsList = rentals.ToList();
            if (!rentalsList.Any())
            {
                throw new InvalidOperationException("No rentals on which perform calculation");
            }

            return rentalsList.Sum(rental => rental.Amount);
        }

        private string FormatLine(Rental rental)
            => $"{rental.Date.ToString("dd-MM-yyyy")} : {rental.Label} | {rental.Amount}{Environment.NewLine}";

        public string DisplayRentals()
        {
            if (!_rentals.Any())
            {
                throw new InvalidOperationException("No rentals on which perform calculation");
            }

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