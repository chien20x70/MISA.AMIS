using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Paging<MISAEntity> where MISAEntity: class
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int? TotalRecord { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int? TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalRecord / PageSize);
            }
        }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Số bản ghi trên một trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Dữ liệu
        /// </summary>
        public IEnumerable<MISAEntity>? Data { get; set; }
    }
}
