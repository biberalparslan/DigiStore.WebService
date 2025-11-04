using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DigiStore.WebService.Infrastructure.Data;

namespace DigiStore.WebService.Infrastructure.Repositories
{
    public class SpExecutor
    {
        private readonly ApplicationDbContext _db;

        public SpExecutor(ApplicationDbContext db)
        {
            _db = db;
        }

        private SqlDbType InferSqlDbType(object? value)
        {
            if (value == null || value == DBNull.Value) return SqlDbType.NVarChar;
            return value switch
            {
                int => SqlDbType.Int,
                long => SqlDbType.BigInt,
                short => SqlDbType.SmallInt,
                byte => SqlDbType.TinyInt,
                bool => SqlDbType.Bit,
                decimal => SqlDbType.Decimal,
                double => SqlDbType.Float,
                float => SqlDbType.Real,
                DateTime => SqlDbType.DateTime,
                Guid => SqlDbType.UniqueIdentifier,
                _ => SqlDbType.NVarChar
            };
        }

        public async Task<List<Dictionary<string, object?>>> ExecuteReaderAsync(string storedProcedure, Dictionary<string, object?>? parameters = null)
        {
            var list = new List<Dictionary<string, object?>>();
            using var conn = (SqlConnection)_db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = storedProcedure;
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var kv in parameters)
                {
                    var p = new SqlParameter(kv.Key.StartsWith("@") ? kv.Key : "@" + kv.Key, kv.Value ?? DBNull.Value);
                    p.SqlDbType = InferSqlDbType(kv.Value);
                    cmd.Parameters.Add(p);
                }
            }

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object?>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    var val = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                    row[name] = val;
                }
                list.Add(row);
            }

            return list;
        }

        public async Task<int> ExecuteNonQueryAsync(string storedProcedure, Dictionary<string, object?>? parameters = null)
        {
            using var conn = (SqlConnection)_db.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = storedProcedure;
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var kv in parameters)
                {
                    var p = new SqlParameter(kv.Key.StartsWith("@") ? kv.Key : "@" + kv.Key, kv.Value ?? DBNull.Value);
                    p.SqlDbType = InferSqlDbType(kv.Value);
                    cmd.Parameters.Add(p);
                }
            }

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}
