using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Position
    {
        /// <summary>
        /// Mã Id Nhóm khách hàng.
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên của nhóm khách hàng.
        /// </summary>
        public string PositionIdName { get; set; }

        /// <summary>
        /// Mô tả.
        /// </summary>
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
