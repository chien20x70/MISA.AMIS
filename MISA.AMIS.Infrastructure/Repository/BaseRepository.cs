using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MySqlConnector;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity> where MISAEntity : class
    {
        string tableName = typeof(MISAEntity).Name;
        IConfiguration _configuration;
        string connectionDb;
        IDbConnection dbConnection;
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionDb = _configuration.GetConnectionString("connectionDB");
        }
        public int Delete(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Delete{tableName}";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId);
                var rowEffects = dbConnection.Execute(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }

        public IEnumerable<MISAEntity> GetAll()
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Get{tableName}s";
                var entities = dbConnection.Query<MISAEntity>(sql, commandType: CommandType.StoredProcedure);
                return entities;
            }
        }

        public MISAEntity GetById(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Get{tableName}ById";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId);
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return entity;
            }
        }

        public IEnumerable<MISAEntity> GetMISAEntities(int pageSize, int pageIndex)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Get{tableName}Paging";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@m_PageIndex", pageIndex);
                dynamicParameters.Add("@m_PageSize", pageSize);
                var entities = dbConnection.Query<MISAEntity>(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return entities;
            }
        }

        public int Insert(MISAEntity entity)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Insert{tableName}";
                var rowEffects = dbConnection.Execute(sql, entity, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }

        public int Update(MISAEntity entity)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Update{tableName}";
                var rowEffects = dbConnection.Execute(sql, entity, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }
    }
}
