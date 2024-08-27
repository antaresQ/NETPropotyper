using Microsoft.Data.SqlClient;
using Sample.Core.Interfaces;
using System.Data;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Data
{
    public class MSSQLDBContext : IMSSQLDBAcess
    {
        public async Task<List<T>> ResultListAsync<T, U>(string storedProc, U param, string connString, int? commTimeOut)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                var rows = await conn.QueryAsync<T>(storedProc, param, commandType: CommandType.StoredProcedure, commandTimeout: commTimeOut);
                return rows.ToList();
            }
        }

        public async Task<T> ResultAsync<T, U>(string storedProc, U param, string connString, int? commTimeOut)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                var rows = await conn.QueryAsync<T>(storedProc, param, commandType: CommandType.StoredProcedure, commandTimeout: commTimeOut);
                return rows.FirstOrDefault();
            }
        }

        public async Task<bool> SaveAsync<T, U>(string storedProc, U param, string connString, int? commTimeOut)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                var rows = await conn.ExecuteAsync(storedProc, param, commandType: CommandType.StoredProcedure, commandTimeout: commTimeOut);
                return (rows != 0);
            }
        }

        public async Task<bool> IsExist<T>(string storedProc, T param, string connString, int? commTimeOut)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                var rows = await conn.QueryAsync<T>(storedProc, param, commandType: CommandType.StoredProcedure, commandTimeout: commTimeOut);
                return rows.Any();
            }

        }

        public async Task<dynamic> TwoListResults<T1, T2, U>(string storedProc, U param, string connString, int? commTimeOut)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                var sets = await conn.QueryMultipleAsync(storedProc, param, commandType: CommandType.StoredProcedure, commandTimeout: commTimeOut);
                
                List<T1> list1 = sets.Read<T1>().ToList();
                List<T2> list2 = sets.Read<T2>().ToList();


                return new {list1, list2};
            }
        }
    }
}
