using CW20.Domain;

namespace CW20.Services;

public interface IRegistrationService
{
    /// <summary>
    /// ثبت نام کاربر در رویداد
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eventId"></param>
    /// <returns></returns>
    Task RegisterUserToEvent(int userId, int eventId);

    /// <summary>
    /// کنسل رویداد با ایدی 
    /// </summary>
    /// <param name="registerationId"></param>
    /// <returns></returns>
    Task CancelRegistration(int registerationId);

    /// <summary>
    /// دریافت کاربران یک رویداد
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    Task<List<User>> GetEventParticipants(int eventId);

    /// <summary>
    /// دریافت رویداد یک کاربر 
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    Task<List<EventsTable>> GetUserEvents(int userId);

}
