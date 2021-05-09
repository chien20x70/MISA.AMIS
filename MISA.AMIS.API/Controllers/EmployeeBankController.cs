using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeBankController : BaseController<EmployeeBank>
    {
        IEmployeeBankRepository _employeeBankRepository;
        IEmployeeBankService _employeeBankService;
        public EmployeeBankController(IEmployeeBankRepository employeeBankRepository,
        IEmployeeBankService employeeBankService) : base(employeeBankRepository, employeeBankService)
        {
            _employeeBankRepository = employeeBankRepository;
            _employeeBankService = employeeBankService;
        }
    }
}
