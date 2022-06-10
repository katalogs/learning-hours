using Seam_TripService.Exception;
using Seam_TripService.Trip;
using Seam_TripService.User;
using Xunit;

namespace Seam_TripService_Test;

public class TripServiceTest
{
    [Fact]
    public void should_throw_exception_when_user_is_not_logged()
    {
        // arrange
        TripService tripService = new TestableTripService();
        
        // act
        // assert
        Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(new User()));
    }

    public class TestableTripService : TripService
    {
        protected override User? GetLoggedUser()
        {
            return null;
        }
    }
}