using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Service
{
    public class DepartmentService: BaseService<Department>, IDepartmentService
    {       
        public DepartmentService(IDepartmentRepository departmentRepository): base(departmentRepository)
        {

        }
    }
}
