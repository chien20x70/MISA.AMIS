using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired: Attribute
    {
        public string MsgErrorEmpty;
        public MISARequired(string msgErrorEmpty)
        {
            MsgErrorEmpty = msgErrorEmpty;
        }
    }
}
