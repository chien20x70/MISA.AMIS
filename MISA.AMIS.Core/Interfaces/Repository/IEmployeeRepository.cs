using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        /// <summary>
        /// Kiểm tra trùng attribute của đối tượng nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã code nhân viên</param>
        /// <param name="employeeId">Mã ID nhân viên</param>
        /// <param name="http">Phương thứ PUT hay POST</param>
        /// <param name="attributeValue">Giá trị của attribute</param>
        /// <returns>TRUE hoặc FALSE</returns>
        /// Created By: NXCHIEN 07/05/2021
        public bool CheckEmployeeAttributeExist(string employeeCode, Guid? employeeId, HTTPType http, string attributeValue);

        public string GetEmployeeCodeMax();
    }
}
