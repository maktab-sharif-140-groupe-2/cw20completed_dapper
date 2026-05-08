using CW20.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW20.Business.Interfaces
{
    public interface IEventDapperService
    {

        Task<List<EventsTable>> GetAllEvents();
        Task<EventsTable> GetEventById(int id);
        Task<int> AddEvent(EventsTable eventsTable);
        Task<int> UpdateEvent(EventsTable eventsTable);
        Task<int> DeleteEvent(int id);


    }
}
