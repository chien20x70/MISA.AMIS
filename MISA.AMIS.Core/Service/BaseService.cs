using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public int Delete(Guid entityId)
        {
            return _baseRepository.Delete(entityId);
        }

        public IEnumerable<MISAEntity> GetAll()
        {
            return _baseRepository.GetAll();
        }

        public MISAEntity GetById(Guid entityId)
        {
            return _baseRepository.GetById(entityId);
        }

        public IEnumerable<MISAEntity> GetMISAEntities(int pageSize, int pageIndex)
        {
            return _baseRepository.GetMISAEntities(pageSize, pageIndex);
        }

        public int Insert(MISAEntity entity)
        {
            return _baseRepository.Insert(entity);
        }

        public int Update(MISAEntity entity)
        {
            return _baseRepository.Update(entity);
        }
    }
}
