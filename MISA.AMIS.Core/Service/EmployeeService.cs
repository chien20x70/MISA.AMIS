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
            using var package = new ExcelPackage(stream);
            var workSheet = package.Workbook.Worksheets.Add("DANH SÁCH NHÂN VIÊN");

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
                range.Value = "DANH SÁCH NHÂN VIÊN";
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 16;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // style cho excel.
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 2].Value = "Mã nhân viên";
            workSheet.Cells[3, 3].Value = "Tên nhân viên";
            workSheet.Cells[3, 4].Value = "Giới tính";
            workSheet.Cells[3, 5].Value = "Ngày sinh";
            workSheet.Cells[3, 6].Value = "Chức danh";
            workSheet.Cells[3, 7].Value = "Tên đơn vị";
            workSheet.Cells[3, 8].Value = "Số tài khoản";
            workSheet.Cells[3, 9].Value = "Tên ngân hàng";

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
                workSheet.Cells[i + 4, 4].Value = e.Gender;
                workSheet.Cells[i + 4, 5].Value = e.DateOfBirth?.ToString("dd/MM/yyyy");
                workSheet.Cells[i + 4, 6].Value = e.PositionName;
                workSheet.Cells[i + 4, 7].Value = e.DepartmentId;
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
            var checkCodeExist = _employeeRepository.CheckEmployeeAttributeExist("EmployeeCode", employeeId, http, employeeCode);
            // Kiểm tra trùng hay không
            if (checkCodeExist)
            {
                throw new EmployeeExceptions(Properties.Resources.Msg_Code_Exist);
            }


            //Check trùng IDentifyNumber
            // Khởi tạo giá trị
            var identifyNumber = entity.IdentifyNumber;
            var checkIdentifyNumberExist = _employeeRepository.CheckEmployeeAttributeExist("IdentifyNumber", employeeId, http, identifyNumber);
            // Kiểm tra trùng hay không
            if (checkIdentifyNumberExist)
            {
                throw new EmployeeExceptions(Properties.Resources.Msg_IdentifyNumber_Exist);
            }

            // Check trùng số điện thoại
            // Khởi tạo giá trị
            var phoneNumber = entity.PhoneNumber;
            var checkphoneNumberExist = _employeeRepository.CheckEmployeeAttributeExist("PhoneNumber", employeeId, http, phoneNumber);
            // Kiểm tra trùng hay không
            if (checkphoneNumberExist)
            {
                throw new EmployeeExceptions(Properties.Resources.Msg_Phone_Exist);
            }
        }
    }
}
