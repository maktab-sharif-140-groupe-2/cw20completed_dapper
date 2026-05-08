using CW20.Business.Interfaces;
using CW20.DbConnectionString;
using CW20.Domain;
using Dapper;
using Microsoft.Data.SqlClient;
namespace CW20.Business.Services;
public class RegistrationDapperService : IRegistrationDapperService
{

    public async Task<int> CancelRegistration(int registrationId)
    {

        using var connection = DataBaseConnection.CreateConnection();

        await connection.OpenAsync();

        var query = @"UPDATE Registrations SET Isdeleted = 1 WHERE Id = @Id AND Isdeleted <>1 ";

        return await connection.ExecuteAsync(query, new
        {
            Id = registrationId
        });
    }

    public async Task<List<User>> GetEventParticipants(int eventId)
    {

        using var connection = DataBaseConnection.CreateConnection();

        connection.Open();

        var query = @"Select * From Users as U
               WHERE U.Id IN (
               SELECT R.UserId FROM Registrations as R
               WHERE R.EventId=@EventId ) ";

        await connection.OpenAsync();

        var users = await connection.QueryAsync<User>(query);
        return users.ToList();
    }

    public async Task<List<EventsTable>> GetUserEvents(int userId)
    {
        var userEvent = new List<EventsTable>();

        using var connection = DataBaseConnection.CreateConnection();

        var query = @"SELECT * FROM EventsTable ev
                      WHERE ev.id in (select EventId FROM
                      Registrations r where r.userid=@UserId)";

        await connection.OpenAsync();

        var events = await connection.QueryAsync<EventsTable>(query);
        return events.ToList();

    }

    public async Task<int> RegisterUserToEvent(int userId, int eventId)
    {

        using var connection = DataBaseConnection.CreateConnection();
        connection.Open();
        var query = @"INSERT INTO Registrations
                      (
                          UserId,
                          EventId, 
                          RegisterDate,
                          Isdeleted
                      )
                       VALUES
                      (
                        @UserId,
                        @EventId, 
                        @RegisterDate,
                        @Isdeleted
                      
                      )";

      return await connection.ExecuteAsync(query, new
        {
            UserId=userId,
            EventId=eventId,
            RegisterDate= DateTime.UtcNow,
            Isdeleted=false,
        });
    }
}
