using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MISA.AMIS.Core.Enums;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class EmployeeRepository: BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration): base(configuration)
        {

        }

        /// <summary>
        /// Kiểm tra trùng attribute của đối tượng nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã code nhân viên</param>
        /// <param name="employeeId">Mã ID nhân viên</param>
        /// <param name="http">Phương thứ PUT hay POST</param>
        /// <param name="attributeValue">Giá trị của attribute</param>
        /// <returns>TRUE hoặc FALSE</returns>
        /// Created By: NXCHIEN 07/05/2021
        public bool CheckEmployeeAttributeExist(string attribute, Guid? employeeId, HTTPType http, string attributeValue)
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                DynamicParameters parameters = new DynamicParameters();
                if(http == HTTPType.POST)
                {
                    parameters.Add($"@{attribute}", attributeValue);
                    parameters.Add("@employeeId", null);
                }
                else if(http == HTTPType.PUT)
                {
                    parameters.Add($"@{attribute}", attributeValue);
                    parameters.Add("@employeeId", employeeId);
                }
                var sqlCommand = $"Proc_Check{attribute}Exist";
                var check = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, parameters, commandType: CommandType.StoredProcedure);
                return check;
            }
        }

        /// <summary>
        /// Lấy ra EmployeeCode lớn nhất trong DB
        /// </summary>
        /// <returns>EmployeeCode</returns>
        /// Created By: NXCHIEN 09/05/2021
        public string GetEmployeeCodeMax()
        {
            using (dbConnection = new MySqlConnection(connectionDb))
            {               
                var sqlCommand = $"Proc_GetEmployeeCodeMax";
                var employeeCode = dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
                return employeeCode;
            }
        }

        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="pageSize">số lượng nhân viên / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: NXCHIEN (09/05/2021)
        public Paging<Employee> GetEmployees(int pageSize, int pageIndex, string filter)
        {
            var res = new Paging<Employee>()
            {
                Page = pageIndex,
                PageSize = pageSize
            };
            using (dbConnection = new MySqlConnection(connectionDb))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@filter", filter);
                // Tính tổng nhân viên.
                int? totalRecord = dbConnection.QueryFirstOrDefault<int>("Proc_GetTotalEmployees", parameters, commandType: CommandType.StoredProcedure);
                if (totalRecord == null)
                {
                    return res;
                }
                res.TotalRecord = totalRecord;
                // Lấy danh sách nhân viên.
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageIndex", pageIndex);
                dynamicParameters.Add("@pageSize", pageSize);
                dynamicParameters.Add("@filter", filter);
                var employees = dbConnection.Query<Employee>("Proc_GetEmployeeFilter", dynamicParameters, commandType: CommandType.StoredProcedure);
                res.Data = employees;
                return res;
            }   
        }
    }
}
