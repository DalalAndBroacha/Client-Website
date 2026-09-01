using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using PieAPI.Repository.Interface;

namespace PieAPI.Repository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IConfiguration _configuration;
        public DapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<object> GetAll<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection("Data Source=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("DataSource")) + "Initial Catalog=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("InitialCatalog")) + "User ID=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("UserID")) + "Password=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("Password")) + "");
            return db.Query<object>(query, sp_params, commandType: commandType, commandTimeout: _configuration.GetValue<int>("TimeOut")).ToList();
        }

        public object Get<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection("Data Source=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("DataSource")) + "Initial Catalog=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("InitialCatalog")) + "User ID=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("UserID")) + "Password=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("Password")) + "");
            return db.Query<object>(query, sp_params, commandType: commandType, commandTimeout: _configuration.GetValue<int>("TimeOut")).FirstOrDefault();
        }

        public object GetMultiResultSet(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection("Data Source=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("DataSource")) + "Initial Catalog=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("InitialCatalog")) + "User ID=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("UserID")) + "Password=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("Password")) + "");
            return db.QueryMultiple(query, sp_params, commandType: commandType, commandTimeout: _configuration.GetValue<int>("TimeOut"));
        }

        public T execute_sp<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using (IDbConnection dbConnection = new SqlConnection("Data Source=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("DataSource")) + "Initial Catalog=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("InitialCatalog")) + "User ID=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("UserID")) + "Password=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("Password")) + ""))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                using var transaction = dbConnection.BeginTransaction();
                try
                {
                    dbConnection.Query<T>(query, sp_params, commandType: commandType, transaction: transaction);
                    result = sp_params.Get<T>("retVal"); //get output parameter value
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            };
            return result;
        }



    }
}
