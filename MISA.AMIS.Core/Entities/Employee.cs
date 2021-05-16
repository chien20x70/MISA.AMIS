using MISA.AMIS.Core.CustomAttribute;
using MISA.AMIS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Employee
    {
        /// <summary>
        /// Mã ID nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã code nhân viên
        /// </summary>
        [MISARequired("")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MISARequired("")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public GenderType Gender { get; set; }

        public string GenderName {
            get
            {
                switch (Gender)
                {
                    case GenderType.Female:
                        return Properties.GenderResource.Female;

                    case GenderType.Male:
                        return Properties.GenderResource.Male;

                    case GenderType.Other:
                        return Properties.GenderResource.Other;

                    default:
                        return Properties.GenderResource.Default;
                }
            }
        }

        /// <summary>
        /// Số chứng minh thư
        /// </summary>
        public string IdentifyNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? DateOfIN { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string PlaceOfIN { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MISARequired("")]
        [MISAEmail("")]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MISARequired("")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Mã Id phòng ban
        /// </summary>
        [MISARequired("")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Tên Ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string BankBranchName { get; set; }

        /// <summary>
        /// Tỉnh thành phố.
        /// </summary>
        public string BankProvinceName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
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
