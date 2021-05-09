using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class BaseController<MISAEntity> : ControllerBase where MISAEntity: class
    {
        IBaseRepository<MISAEntity> _baseRepository;
        IBaseService<MISAEntity> _baseService;
        static string tableName = typeof(MISAEntity).Name;
        public BaseController(IBaseRepository<MISAEntity> baseRepository,
        IBaseService<MISAEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }

        /// <summary>
        /// Lấy ra danh sách của đối tượng
        /// </summary>
        /// <returns>
        ///     - Thành công: 200 - Danh sách đối tượng
        ///     - NoContent: 204
        /// </returns>
        /// Created By: NXCHIEN 07/05/2021
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseRepository.GetAll();
            if (entities.Count() > 0)
            {
                return Ok(entities);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Lấy ra 1 đối tượng theo ID
        /// </summary>
        /// <param name="id">Mã id của đối tượng</param>
        /// <returns>
        ///     - Thành công: 200 - Đối tượng có mã ID như trên
        ///     - NoContent: 204
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {

            var entity = _baseRepository.GetById(id);
            if (entity != null)
            {
                return Ok(entity);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Thêm mới 1 đối tượng 
        /// </summary>
        /// <param name="entity">đối tượng cần thêm</param>
        /// <returns>
        ///     - Thành công: 201 - số dòng trong bảng trong DB bị ảnh hưởng
        ///     - NoContent: 204
        /// </returns>
        /// Created By: NXCHIEN 07/05/2021
        [HttpPost]
        public IActionResult Post([FromBody] MISAEntity entity)
        {
            var rowAffects = _baseService.Insert(entity);
            if (rowAffects > 0)
            {
                return StatusCode(201, rowAffects);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Sửa 1 đối tượng
        /// </summary>
        /// <param name="id">Mã ID của đối tượng cần sửa</param>
        /// <param name="entity">Đối tượng cần sửa</param>
        /// <returns>
        ///     - Thành công: 200 - Đối tượng vừa được sửa
        ///     - NoContent: 204
        /// </returns>
        /// Created By: NXCHIEN 07/05/2021
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] MISAEntity entity)
        {
            AssignEntityIdInEntity(id, entity);
            var rowAffects = _baseService.Update(entity);
            if (rowAffects > 0)
            {
                return Ok(entity);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Xóa 1 đối tượng
        /// </summary>
        /// <param name="id">Mã ID của dối tượng muốn xóa</param>
        /// <returns>
        ///     - Thành công: 200 - Message thông báo xóa thành công.
        ///     - NoContent: 204
        /// </returns>
        /// Created By: NXCHIEN 07/05/2021
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var rowAffects = _baseService.Delete(id);
            if (rowAffects > 0)
            {
                return Ok(Properties.Resources.Msg_Delete_Success);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Phân trang đối tượng
        /// </summary>
        /// <param name="pageSize">số đối tượng trên 1 trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <returns>
        ///     - Thành công: 200 - Danh sách đối tượng
        ///     - NoContent: 204
        /// </returns>
        /// Created By: NXCHIEN 07/05/2021
        [HttpGet("Paging")]
        public IActionResult Filters(int pageSize, int pageIndex)
        {
            //Lấy tất cả bản ghi trong DB
            var limit = _baseRepository.GetAll().Count();
            //Kiểm tra nếu số khách trên trang hoặc vị trí trang < 1 thì trả về BadRequest
            if (pageSize < 1 || pageIndex < 1)
            {
                return BadRequest();
            }
            // Kiểm tra nếu số khách/trang * vị trí trang < tổng khách + số khách/trang thì trả về NoContent.
            else if (pageSize * pageIndex >= (limit + pageSize))      //limit =245 total =250        245+10
            {
                return NoContent();
            }
            var entity = _baseService.GetMISAEntities(pageSize, pageIndex);
            if (entity != null)
            {
                return Ok(entity);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Gán id cho 1 đối tượng
        /// </summary>
        /// <param name="id">Mã ID cần gán</param>
        /// <param name="entity">Đối tượng cần gán</param>
        /// Created By: NXCHIEN 07/05/2021
        public static void AssignEntityIdInEntity(Guid id, MISAEntity entity)
        {
            // lấy tất cả property của đối tượng;
            var properties = typeof(MISAEntity).GetProperties();
            // Duyệt tất cả property của đối tượng
            foreach (var item in properties)
            {
                // Kiểm tra tên của property trùng với entityId thì gán giá trị của property = id;
                if (item.Name == $"{tableName}Id")
                {
                    item.SetValue(entity, id);
                }
            }
        }       
    }
}
