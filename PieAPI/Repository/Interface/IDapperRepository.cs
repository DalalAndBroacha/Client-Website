using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PieAPI.Repository.Interface
{
    public interface IDapperRepository
    {
        public T execute_sp<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        public List<object> GetAll<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);

        public object Get<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        public object GetMultiResultSet(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);

    }
}
