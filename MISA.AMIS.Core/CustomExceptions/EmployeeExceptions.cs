using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.CustomExceptions
{
    public class EmployeeExceptions: Exception
    {
        public EmployeeExceptions(string msg): base(msg)
        {

        }
    }
}
