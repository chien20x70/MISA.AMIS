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
        string tableName = typeof(MISAEntity).Name;
        public BaseController(IBaseRepository<MISAEntity> baseRepository,
        IBaseService<MISAEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }


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


        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] MISAEntity entity)
        {
            // lấy tất cả property cảu đối tượng;
            var properties = typeof(MISAEntity).GetProperties();
            // Duyệt tất cả property của đối tượng
            foreach (var item in properties)
            {
                // Kiểm tra tên của property trùng với entityId thì gán giá trị cảu property = id;
                if (item.Name == $"{tableName}Id")
                {
                    item.SetValue(entity, id);
                }
            }
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


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var rowAffects = _baseService.Delete(id);
            if (rowAffects > 0)
            {
                return Ok("Xoas thanh cong");
            }
            else
            {
                return NoContent();
            }
        }


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
    }
}
