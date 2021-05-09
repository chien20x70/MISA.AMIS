using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class EmployeeBank
    {
        public Guid EmployeeBankId { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string Branch { get; set; }
        public string BankCity { get; set; }
        public int Status { get; set; }
        public Guid EmployeeId { get; set; }
        public string StatusName { get; set; }

    }
}
