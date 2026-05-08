using CW20.DbConnectionString;
using CW20.Domain;
using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW20.Business.GenericServices
{
    public abstract class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        public async Task<List<T>> GetAll()
        {
            await using var conn = DataBaseConnection.CreateConnection();
            await conn.OpenAsync();
            var query = $"Select * From {GetTableName()}";
            var entities = await conn.QueryAsync<T>(query);

            return entities.ToList();
        }

        public async Task<T?> GetById(int id)
        {
            await using var connection = DataBaseConnection.CreateConnection();

            var query = File.ReadAllText(@"C:\Users\ASUS\Desktop\projects\may5thCW20\CW20\CW20.Business\Query\GetbyId.sql");

            await connection.OpenAsync();

            var entity = await connection.QueryFirstOrDefaultAsync<T>(query, new { GetTableName = GetTableName() });

            return entity;
        }

        public async Task SoftDelete(int id)
        {
            await using var connection = DataBaseConnection.CreateConnection();
            var query = $@"
                            UPDATE  {GetTableName()} 
                            SET Isdeleted = 1
                            WHERE Id = @Id AND Isdeleted<>1
                         ";

            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(query, new
            {
                Id = id

            });
        }
        protected abstract string GetTableName();
    }
}
