using Microsoft.Data.SqlClient;

namespace CW20.DbConnectionString;

public static class DataBaseConnection
{
    private const string _connectionString =
    "Data Source=LAPTOP-2KIVDEID;Initial Catalog=System_Management_Event;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

    public static SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

}
