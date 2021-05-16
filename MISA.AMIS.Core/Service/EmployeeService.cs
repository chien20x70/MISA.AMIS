using MISA.AMIS.Core.CustomExceptions;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Service
{
    public class EmployeeService: BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository):base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Export file excel
        /// </summary>
        /// <param name="pageSize">số nhân viên / trang</param>
        /// <param name="pageIndex">Trang số bao nhiêu</param>
        /// <param name="filter">lọc bằng chuỗi string</param>
        /// <returns>Stream</returns>
        /// CreatedBy: NXCHIEN 11/05/2021
        public Stream ExportExcel(int pageSize, int pageIndex, string filter)
        {
            var res = _employeeRepository.GetEmployees(pageSize, pageIndex, filter);
            var list = res.Data.ToList();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(stream);            
            var workSheet = package.Workbook.Worksheets.Add(Properties.ExcelResource.ExcelName);

            // set độ rộng col
            workSheet.Column(1).Width = 5;
            workSheet.Column(2).Width = 15;
            workSheet.Column(3).Width = 30;
            workSheet.Column(4).Width = 10;
            workSheet.Column(5).Width = 15;
            workSheet.Column(6).Width = 30;
            workSheet.Column(7).Width = 30;
            workSheet.Column(8).Width = 15;
            workSheet.Column(9).Width = 30;

            using (var range = workSheet.Cells["A1:I1"])
            {
                range.Merge = true;
                range.Value = Properties.ExcelResource.ExcelName;
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 16;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // style cho excel.
            workSheet.Cells[3, 1].Value = Properties.ExcelResource.ExcelColumnSTT;
            workSheet.Cells[3, 2].Value = Properties.ExcelResource.ExcelColumnMNV;
            workSheet.Cells[3, 3].Value = Properties.ExcelResource.ExcelColumnTNV;
            workSheet.Cells[3, 4].Value = Properties.ExcelResource.ExcelColumnGT;
            workSheet.Cells[3, 5].Value = Properties.ExcelResource.ExcelColumnNS;
            workSheet.Cells[3, 6].Value = Properties.ExcelResource.ExcelColumnCD;
            workSheet.Cells[3, 7].Value = Properties.ExcelResource.ExcelColumnTDV;
            workSheet.Cells[3, 8].Value = Properties.ExcelResource.ExcelColumnSTK;
            workSheet.Cells[3, 9].Value = Properties.ExcelResource.ExcelColumnTNH;

            using (var range = workSheet.Cells["A3:I3"])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }


            int i = 0;
            // đổ dữ liệu từ list vào.
            foreach (var e in list)
            {
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 2].Value = e.EmployeeCode;
                workSheet.Cells[i + 4, 3].Value = e.FullName;
                workSheet.Cells[i + 4, 4].Value = e.GenderName;
                workSheet.Cells[i + 4, 5].Value = e.DateOfBirth?.ToString(Properties.ExcelResource.ExcelFormatDate);
                workSheet.Cells[i + 4, 6].Value = e.PositionName;
                workSheet.Cells[i + 4, 7].Value = e.DepartmentName;
                workSheet.Cells[i + 4, 8].Value = e.BankAccountNumber;
                workSheet.Cells[i + 4, 9].Value = e.BankName;

                using (var range = workSheet.Cells[i + 4, 1, i + 4, 9])
                {
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
                i++;
            }

            package.Save();
            stream.Position = 0;
            return package.Stream;
        }

        public Employee GetDuplicateEmployee(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            var employeeCode = GetEmployeeCodeMax();
            employee.EmployeeCode = employeeCode;

            return employee;
        }

        /// <summary>
        /// Lấy ra EmployeeCode lớn nhất trong DB
        /// </summary>
        /// <returns>EmployeeCode</returns>
        /// Created By: NXCHIEN 09/05/2021
        public string GetEmployeeCodeMax()
        {
            string codeMax = _employeeRepository.GetEmployeeCodeMax();
            string keyChar = string.Empty;
            string numberChar = string.Empty;

            if (codeMax == null)
            {
                return "NV-00001";
            }

            // Tách chữ và số ra hai mảng
            for (int i = 0; i < codeMax.Length; i++)
            {
                if (Char.IsDigit(codeMax[i]))
                {
                    numberChar += codeMax[i];
                }
                else
                {
                    keyChar += codeMax[i];
                }
            }

            // Tăng code lên 1 đơn vị
            // NV-00001         valueCode = 2   l = 5-1=4   result = NV-00002
            // NV-09             valueCode = 10   l =2-2=0   result = NV-10
            int valueCode = int.Parse(numberChar) + 1;

            int l = numberChar.Length - valueCode.ToString().Length;
            var result = keyChar;
            if(l > 0)
            {
                for (int i = 0; i < l; i++)
                {
                    result += "0";
                }
            }

            result += valueCode.ToString();

            return result;
        }

        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="pageSize">số lượng nhân viên / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: NXCHIEN (09/05/2021)
        public Paging<Employee> GetEmployees(int pageSize, int pageIndex, string filter)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                throw new EmployeeExceptions(Properties.Resources.Msg_Param_Error);
            }
            return _employeeRepository.GetEmployees(pageSize, pageIndex, filter);
        }

        /// <summary>
        /// Validate dữ liệu riêng từng đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <param name="http">Phương thức POST hay PUT</param>
        /// Created By: NXCHIEN 07/05/2021
        protected override void CustomValidate(Employee entity, HTTPType http)
        {

            // Check trùng mã code
            // Khởi tạo giá trị
            var employeeCode = entity.EmployeeCode;
            var employeeId = entity.EmployeeId;
            var checkCodeExist = _employeeRepository.CheckEmployeeAttributeExist(Properties.AttributeResource.EmployeeCode, employeeId, http, employeeCode);
            // Kiểm tra trùng hay không
            if (checkCodeExist)
            {
                throw new EmployeeExceptions(Properties.Resources.Msg_Code_Exist);
            }


            //Check trùng IDentifyNumber
            // Khởi tạo giá trị
            var identifyNumber = entity.IdentifyNumber;
            var checkIdentifyNumberExist = _employeeRepository.CheckEmployeeAttributeExist(Properties.AttributeResource.IdentifyNumber, employeeId, http, identifyNumber);
            // Kiểm tra trùng hay không
            if (checkIdentifyNumberExist)
            {
                throw new EmployeeExceptions(Properties.Resources.Msg_IdentifyNumber_Exist);
            }

            // Check trùng số điện thoại
            // Khởi tạo giá trị
            var phoneNumber = entity.PhoneNumber;
            var checkphoneNumberExist = _employeeRepository.CheckEmployeeAttributeExist(Properties.AttributeResource.PhoneNumber, employeeId, http, phoneNumber);
            // Kiểm tra trùng hay không
            if (checkphoneNumberExist)
            {
                throw new EmployeeExceptions(Properties.Resources.Msg_Phone_Exist);
            }

            //Check trùng Email
            var email = entity.Email;
            var checkEmailExist = _employeeRepository.CheckEmployeeAttributeExist(Properties.AttributeResource.Email, employeeId, http, email);
            if (checkEmailExist)
            {
                throw new EmployeeExceptions(Properties.Resources.Msg_Email_Exist);
            }
        }
    }
}
