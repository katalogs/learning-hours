﻿using Seam_TripService.Exception;
using Seam_TripService.User;

namespace Seam_TripService.Trip;

public class TripService
{
    public List<Trip> GetTripsByUser(User.User user)
    {
        List<Trip> tripList = new List<Trip>();
        User.User loggedUser = UserSession.GetInstance().GetLoggedUser();
        bool isFriend = false;
        if (loggedUser != null)
        {
            foreach(User.User friend in user.GetFriends())
            {
                if (friend.Equals(loggedUser))
                {
                    isFriend = true;
                    break;
                }
            }
            if (isFriend)
            {
                tripList = TripDAO.FindTripsByUser(user);
            }
            return tripList;
        }
        else
        {
            throw new UserNotLoggedInException();
        }
    }
}