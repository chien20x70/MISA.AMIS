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
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string IdentifyNumber { get; set; }
        public DateTime DateOfIN { get; set; }
        public string PlaceOfIN { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneStatic { get; set; }
        public string PositionName { get; set; }
        public Guid DepartmentId { get; set; }
        public string Address { get; set; }
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
