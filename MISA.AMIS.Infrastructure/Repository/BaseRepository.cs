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
        protected string tableName = typeof(MISAEntity).Name;
        protected IConfiguration _configuration;
        protected string connectionDb;
        protected IDbConnection dbConnection;
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionDb = _configuration.GetConnectionString("connectionDB");
        }

        /// <summary>
        /// Xóa 1 đối tượng.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (07/05/2021)
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

        /// <summary>
        /// Lấy danh sách tất cả các đối tượng.
        /// </summary>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public IEnumerable<MISAEntity> GetAll()
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Get{tableName}s";
                var entities = dbConnection.Query<MISAEntity>(sql, commandType: CommandType.StoredProcedure);
                return entities;
            }
        }

        /// <summary>
        /// Lấy 1 đối tượng theo ID.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>1 đối tượng có mã ID là entityId</returns>
        /// CreatedBy: NXChien (07/05/2021)
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

        /// <summary>
        /// Phân trang đối tượng.
        /// </summary>
        /// <param name="pageSize">số đối tượng trên 1 trang.</param>
        /// <param name="pageIndex">Trang số bao nhiêu.</param>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public IEnumerable<MISAEntity> GetMISAEntities(int pageSize, int pageIndex)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Get{tableName}Paging";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PageIndex", pageIndex);
                dynamicParameters.Add("@PageSize", pageSize);
                var entities = dbConnection.Query<MISAEntity>(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return entities;
            }
        }

        /// <summary>
        /// Thêm mới 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public int Insert(MISAEntity entity)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                var sql = $"Proc_Insert{tableName}";
                var rowEffects = dbConnection.Execute(sql, entity, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }

        /// <summary>
        /// Sửa 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (07/05/2021)
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
