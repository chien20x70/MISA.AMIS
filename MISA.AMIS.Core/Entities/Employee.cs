using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string PositionName { get; set; }
        public string Position { get; set; }
        public string BankAcount { get; set; }
        public string BankName { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Ngày tạo.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Tạo bởi ai.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Ai là người chỉnh sửa.
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
