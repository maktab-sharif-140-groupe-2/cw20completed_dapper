using CW20.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW20.Business.Interfaces
{
    public interface IRegistrationDapperService
    {
       Task<int> RegisterUserToEvent(int userId, int eventId);
       Task<int> CancelRegistration(int registrationId);
       Task<List<User>> GetEventParticipants(int eventId);
       Task<List<EventsTable>> GetUserEvents(int userId);



    }
}
