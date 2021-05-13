using MISA.AMIS.Core.CustomAttribute;
using MISA.AMIS.Core.CustomExceptions;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Service
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity> where MISAEntity : class
    {
        IBaseRepository<MISAEntity> _baseRepository;
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Xóa 1 đối tượng.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public int Delete(Guid entityId)
        {
            return _baseRepository.Delete(entityId);
        }

        /// <summary>
        /// Lấy danh sách tất cả các đối tượng.
        /// </summary>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public IEnumerable<MISAEntity> GetAll()
        {             
            return _baseRepository.GetAll();
        }

        /// <summary>
        /// Lấy 1 đối tượng theo ID.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>1 đối tượng có mã ID là entityId</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public MISAEntity GetById(Guid entityId)
        {
            return _baseRepository.GetById(entityId);
        }

        /// <summary>
        /// Thêm mới 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public int Insert(MISAEntity entity)
        {
            Validate(entity, HTTPType.POST);
            return _baseRepository.Insert(entity);
        }

        /// <summary>
        /// Sửa 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public int Update(MISAEntity entity)
        {
            Validate(entity, HTTPType.PUT);
            return _baseRepository.Update(entity);
        }

        /// <summary>
        /// Phân trang đối tượng.
        /// </summary>
        /// <param name="pageSize">số đối tượng trên 1 trang.</param>
        /// <param name="pageIndex">Trang số bao nhiêu.</param>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public IEnumerable<MISAEntity> GetMISAEntities(int pageSize, int pageIndex)
        {
            return _baseRepository.GetMISAEntities(pageSize, pageIndex);
        }

        /// <summary>
        /// Validate chung dữ liệu
        /// </summary>
        /// <param name="entity">đối tượng truyền vào</param>
        /// <param name="http">Phương thức PUT hay POST</param>
        /// Created By: NXCHIEN 07/05/2021
        private void Validate(MISAEntity entity, HTTPType http)
        {
            // Lấy ra tất cả property của đối tượng
            var properties = typeof(MISAEntity).GetProperties();
            foreach (var property in properties)
            {
                // Lấy ra attribute của đối tượng
                var requiredAttribute = property.GetCustomAttributes(typeof(MISARequired), true);
                if (requiredAttribute.Length > 0)
                {
                    // Lấy ra giá trị của property
                    var propertyValue = property.GetValue(entity);
                    // Kiểm tra nếu giá trị null thì gán thành empty.
                    if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        // Lấy ra message lỗi của attribute.
                        var msgError = (requiredAttribute[0] as MISARequired).MsgErrorEmpty;
                        if (string.IsNullOrEmpty(msgError))
                        {
                            msgError = $"{property.Name} không được phép để trống!";
                        }
                        throw new EmployeeExceptions(msgError);
                    }
                }
                // Kiểm tra độ dài mã Code của property
                var maxLengthAttribute = property.GetCustomAttributes(typeof(MISAMaxLength), true);
                if (maxLengthAttribute.Length > 0)
                {
                    // Lấy ra giá trị của property
                    var propertyValue = property.GetValue(entity);
                    // Lấy ra giá trị truyền vào của MISAMaxLength
                    var maxLength = (maxLengthAttribute[0] as MISAMaxLength).MaxLength;
                    // Kiểm tra độ dài
                    if (propertyValue.ToString().Length > maxLength)
                    {
                        var msgError = (maxLengthAttribute[0] as MISAMaxLength).MsgError_MaxLength;
                        if (string.IsNullOrEmpty(msgError))
                        {
                            msgError = $"Độ dài của {property.Name} phải nhỏ hơn {maxLength}";
                        }
                        throw new EmployeeExceptions(msgError);
                    }
                }
                // Kiểm tra email
                var emailAttribute = property.GetCustomAttributes(typeof(MISAEmail), true);
                if (emailAttribute.Length > 0)
                {
                    // Lấy giá trị email
                    var emailValue = property.GetValue(entity);
                    // Khởi tạo regex và kiểm tra
                    Regex regex = new Regex(Properties.Resources.Regex_String);
                    if (!regex.IsMatch(emailValue.ToString()))
                    {

                        var msgErrorEmail = (emailAttribute[0] as MISAEmail).MsgErrorEmail;
                        if (string.IsNullOrEmpty(msgErrorEmail.ToString()))
                        {
                            msgErrorEmail = property.Name + Properties.Resources.Required_Error_Message;
                        }
                        throw new EmployeeExceptions(msgErrorEmail);
                    }
                }
            }
            CustomValidate(entity, http);
        }

        /// <summary>
        /// Validate dữ liệu riêng từng đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <param name="http">Phương thức POST hay PUT</param>
        /// Created By: NXCHIEN 07/05/2021
        protected virtual void CustomValidate(MISAEntity entity, HTTPType http) { }

    }
}
