using CW20.Domain;
using Microsoft.Data.SqlClient;

namespace CW20.Services;

public class UserServices : IUserService
{
    private readonly string _connectionString;

    public UserServices(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddUserAsync(User user)
    {
        await using var connection = new SqlConnection(_connectionString);

        connection.Open();

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
                               @Isdeleted)";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@FullName", user.FullName);
        command.Parameters.AddWithValue("@Email", user.Email);
        command.Parameters.AddWithValue("@Phone", user.PhoneNumber);
        command.Parameters.AddWithValue("@RegisterDate", user.CreateAt);
        command.Parameters.AddWithValue("@Isdeleted", user.IsDeleted);

        await command.ExecuteNonQueryAsync();

    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = new List<User>();

        await using var connection = new SqlConnection(_connectionString);

        connection.Open();

        var query = $"Select * From Users";

        var command = new SqlCommand(query, connection);
        var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var user = new User();

            user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            user.FullName = reader.GetString(reader.GetOrdinal("FullName"));
            user.Email = reader.GetString(reader.GetOrdinal("Email"));
            user.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
            user.CreateAt = reader.GetDateTime(reader.GetOrdinal("CreatAt"));
            user.IsDeleted = reader.GetBoolean(reader.GetOrdinal("Isdeleted"));

            users.Add(user);
        }

        return users;
    }


    public async Task<User?> GetUserByIdAsync(int id)
    {
        var user = new User();

        await using var connection = new SqlConnection(_connectionString);

        connection.Open();

        var query = @"SELECT * FROM Users WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        var reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();

        if (await reader.IsDBNullAsync(0)) return null;

        user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
        user.FullName = reader.GetString(reader.GetOrdinal("FullName"));
        user.Email = reader.GetString(reader.GetOrdinal("Email"));
        user.PhoneNumber = reader.GetString(reader.GetOrdinal("Phone"));
        user.CreateAt = reader.GetDateTime(reader.GetOrdinal("RegisterDate"));
        user.IsDeleted = reader.GetBoolean(reader.GetOrdinal("Isdeleted"));

        return user;
    }

    public async Task<List<User>> GetUsersPaged(int pageNumber , int pageSize)
    {
        pageSize = pageSize < 1 ? 10 : pageSize;
        pageNumber = pageNumber < 1 ? 1 : pageNumber;

        var users = new List<User>();

        await using var connection = new SqlConnection(_connectionString);

        connection.Open();

        var query = @"SELECT *
                          FROM Users
                          ORDER BY Id
                          OFFSET @PageNumber ROWS FETCH NEXT @PageSize ROWS ONLY;";


        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@PageNumber", (pageNumber - 1) * pageSize);
        command.Parameters.AddWithValue("@PageSize", pageSize);

        var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var user = new User();

            user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            user.FullName = reader.GetString(reader.GetOrdinal("FullName"));
            user.Email = reader.GetString(reader.GetOrdinal("Email"));
            user.PhoneNumber = reader.GetString(reader.GetOrdinal("Phone"));
            user.CreateAt = reader.GetDateTime(reader.GetOrdinal("RegisterDate"));
            user.IsDeleted = reader.GetBoolean(reader.GetOrdinal("Isdeleted"));

            users.Add(user);
        }

        return users;

    }

    public async Task RemoveUserAsync(int id)
    {
        var user = new User();

        await using var connection = new SqlConnection(_connectionString);

        connection.Open();

        var query = @"UPDATE Users SET Isdeleted = 1 WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateUserAsync(User user)
    {

        await using var connection = new SqlConnection(_connectionString);

        connection.Open();


        var query = @"UPDATE Users SET FullName = @FullName  WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", user.Id);

        command.Parameters.AddWithValue("@FullName", user.FullName);

        await command.ExecuteNonQueryAsync();
    }
}
