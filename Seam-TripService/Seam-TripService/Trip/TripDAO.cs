using Seam_TripService.Exception;

namespace Seam_TripService.Trip;

public class TripDAO
{
    public static List<Trip> FindTripsByUser(User.User user)
    {
        throw new DependentClassCallDuringUnitTestException(
            "TripDAO should not be invoked on an unit test.");
    }
}
