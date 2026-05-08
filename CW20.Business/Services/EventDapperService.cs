using CW20.Business.Interfaces;
using CW20.DbConnectionString;
using CW20.Domain;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CW20.Business.Services
{
    public class EventDapperService : IEventDapperService
    {
        public async Task<int> AddEvent(EventsTable eventsTable)
        {
           using  var connection= DataBaseConnection.CreateConnection();
            var query = @"INSERT INTO EventsTable
                                        (Title,
                                        Description,
                                        EventDate,
                                        Location,
                                        Capacity,
                                        Isdeleted)
                                 VALUES(@Title,
                                        @Description,
                                        @EventDate,
                                        @Location, 
                                        @Capacity,
                                        @Isdeleted)

                                    SELECT CAST(SCOPE_IDENTITY() as int)";

           await connection.OpenAsync();

            var result= await connection.QuerySingleAsync(query,eventsTable);

            return result;
        }

        public async Task<int> DeleteEvent(int id)
        {
            using var connection = DataBaseConnection.CreateConnection();

            var query = @"
                            UPDATE  EventsTable 
                            SET Isdeleted = 1
                            WHERE Id = @Id AND Isdeleted<>1
                         ";

            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(query, new
            {
                id = id
                
            });

            return result;

        }

        public async Task<List<EventsTable>> GetAllEvents()
        {
            using var connection = DataBaseConnection.CreateConnection();

            var query = @" SELECT * FROM EventsTable";

           await connection.OpenAsync();

            var result = await connection.QueryAsync<EventsTable>(query);

            return result.ToList();
        }

        public async Task<EventsTable> GetEventById(int id)
        {
            using var connection = DataBaseConnection.CreateConnection();

            var query = @"
                            SELECT * FROM EventsTable
                             WHERE Id = @Id AND Isdeleted<>1
                         ";

            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefault(query, new
            {
                id = id
            });
            

            return result;
        }

        public async Task<int> UpdateEvent(EventsTable eventsTable)
        {
            using var connection = DataBaseConnection.CreateConnection();
            var query = @"
                            UPDATE EventsTable
                            SET
                            Title = @Title,
                            Description = @Description,
                            Location =@Location,
                            Capacity = @Capacity
                            WHERE Id = @Id AND Isdeleted <> 1";

            await connection.OpenAsync();

            var result= await connection.ExecuteAsync(query,eventsTable);

            return result;
        }
    }
}
