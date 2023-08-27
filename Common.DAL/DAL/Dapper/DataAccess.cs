using Common.Classes;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Common.DAL.Dapper
{
    public class DataAccess : BaseClass
    {
        private string _connectionString;
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private static DataAccess _instance;
       
        public static DataAccess GetInstance(string strConn = "")
        {
            if (_instance != null && _instance.Disposed == false)
            {
                return _instance;
            }
            else
            {
                if (string.IsNullOrEmpty(strConn))
                {
                    _instance = new DataAccess();
                }
                else
                {
                    _instance = new DataAccess(strConn);
                }

                return _instance;
            }
        }

        private DataAccess(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection();
            _connection.ConnectionString = _connectionString;
            _connection.Open();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private DataAccess()
        {
            _connectionString = ConnectionStringBuilder.ConnectionString;
            _connection = new SqlConnection();
            _connection.ConnectionString = _connectionString;
            _connection.Open();
        }

        public List<T> GetAllItems<T>(string sqlQuery, CommandType commandType)
        {
            IList<T> items = _connection.Query<T>(sqlQuery, commandType: commandType).ToList();

            return items.ToList();
        }

        public List<T> GetItems<T>(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType)
        {
            return _connection.Query<T>(sqlQuery, dynamicParameters, commandType: commandType).ToList();
        }

        public int ExecuteNonQuery(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType)
        {
            return _connection.Execute(sqlQuery, dynamicParameters, commandType: commandType, transaction: _transaction);
        }

        public object ExecuteScalar(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType)
        {
            return _connection.ExecuteScalar(sqlQuery, dynamicParameters, commandType: commandType, transaction: _transaction);
        }

        public DataTable LoadDataTable(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType)
        {
            var reader = _connection.ExecuteReader(sqlQuery, dynamicParameters, commandType: commandType, transaction: _transaction);
            var dt = new DataTable();

            dt.Load(reader);
            reader.Close();

            return dt;
        }

        /// <summary>
        /// Begin Transaction
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Commit Transaction
        /// </summary>
        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        /// <summary>
        /// Rollback Transaction
        /// </summary>
        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        private void CloseConnection()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _connection.Close();
            _connection.Dispose();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public new void Dispose()
        {
            CloseConnection();
            base.Dispose();
        }
    }
}