using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {
        public string MsgErrorEmpty;
        public MISARequired(string msgErrorEmpty)
        {
            MsgErrorEmpty = msgErrorEmpty;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MISAMaxLength : Attribute
    {
        /// <summary>
        /// ĐỘ dài lớn nhất của chuỗi.
        /// </summary>
        public int MaxLength;

        /// <summary>
        /// Chuỗi message lỗi khi nhập quá độ dài maxlength.
        /// </summary>
        public string MsgError_MaxLength;
        public MISAMaxLength(int maxLength, string msgError_MaxLength)
        {
            MaxLength = maxLength;
            MsgError_MaxLength = msgError_MaxLength;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MISAEmail : Attribute
    {
        /// <summary>
        /// message email lỗi
        /// </summary>
        public string MsgErrorEmail;
        public MISAEmail(string msgErrorEmail)
        {
            MsgErrorEmail = msgErrorEmail;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MISAPhone : Attribute
    {
        /// <summary>
        /// message số điện thoại trùng
        /// </summary>
        public string MsgErrorPhone;
        public MISAPhone(string msgErrorPhone)
        {
            MsgErrorPhone = msgErrorPhone;
        }
    }
}
