
using CW20.Business.Interfaces;
using CW20.DbConnectionString;
using CW20.Domain;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace CW20.Business.Services;

public class UserDapperService : IUserDapperService 
{
    public async Task<int> AddUser(User user)
    {
        using var connection = DataBaseConnection.CreateConnection();

        var query = @" INSERT INTO Users (
                        FullName,
                        Email,
                        Phone,
                        RegisterDate,
                        Isdeleted)
                        values(
                               @FullName,
                               @Email,
                               @Phone,
                               @RegisterDate,
                               @Isdeleted)

                        SELECT CAST(SCOPE_IDENTITY() as int)
                        ";

        await connection.OpenAsync();

        int userId = await connection.QuerySingleAsync<int>(query, user);

        return userId;
    }
    public async Task<int> DeleteUser(int id)
    {
        using var connection = DataBaseConnection.CreateConnection();

        var query = @"UPDATE Users SET Isdeleted = 1 WHERE Id = @Id";

        await connection.OpenAsync();

        var result = await connection.ExecuteAsync(query, new
        {
            Id = id,
        });

        return result;
    }

    public async Task<List<User>> GetAllUsers()
    {
        using var connection = DataBaseConnection.CreateConnection();

        var query = $"Select * From Users";

        await connection.OpenAsync();

        var users = await connection.QueryAsync<User>(query);

        return users.ToList();
    }

    public async Task<User?> GetUserById(int id)
    {
        using var connection = DataBaseConnection.CreateConnection();

        var query = @"SELECT * FROM Users WHERE Id = @Id";

        await connection.OpenAsync();

        var user = await connection.QueryFirstOrDefaultAsync<User>(query);

        return user;
    }

    public async Task<List<User>> GetUsersPaged(int pageNumber, int pageSize)
    {
        pageSize = pageSize < 1 ? 10 : pageSize;
        pageNumber = pageNumber < 1 ? 1 : pageNumber;

        using var connection = DataBaseConnection.CreateConnection();

        var query = @"SELECT *
                          FROM Users
                          ORDER BY Id
                          OFFSET @PageNumber ROWS FETCH NEXT @PageSize ROWS ONLY;";

        await connection.OpenAsync();

        var users = await connection.QueryAsync<User>(query, new
        {
            PageNumber = (pageNumber - 1) * pageSize,
            PageSize = pageSize
        });

        return users.ToList();
    }

    public async Task<int> UpdateUser(User user)
    {
        using var connection = DataBaseConnection.CreateConnection();

        var query = @"UPDATE Users SET FullName = @FullName  WHERE Id = @Id";

        await connection.OpenAsync();

        var result = await connection.ExecuteAsync(query, user);

        return result;
    }
}
