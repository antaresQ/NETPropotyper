using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Interfaces
{
    public interface IDBAccessBase
    {
        Task<List<T>> ResultListAsync<T, U>(string storedProc, U param, string connString, int? commTimeOut);
        Task<T> ResultAsync<T, U>(string storedProc, U param, string connString, int? commTimeOut);
        Task<bool> SaveAsync<T, U>(string storedProc, U param, string connString, int? commTimeOut);
        Task<bool> IsExist<T>(string storedProc, T param, string connString, int? commTimeOut);
    }

    public interface IMSSQLDBAcess: IDBAccessBase
    {
        Task<dynamic> TwoListResults<T1, T2, U>(string storedProc, U param, string connString, int? commTimeOut);
    }

}
