using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Service
{
    public interface IBaseService<MISAEntity> where MISAEntity: class
    {
        public IEnumerable<MISAEntity> GetAll();
        public MISAEntity GetById(Guid entityId);
        public int Insert(MISAEntity entity);
        public int Update(MISAEntity entity);
        public int Delete(Guid entityId);
        public IEnumerable<MISAEntity> GetMISAEntities(int pageSize, int pageIndex);
    }
}
