using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyTypeController : ControllerBase
    {
        private IBodyTypeService _bodyTypeService;

        public BodyTypeController(IBodyTypeService bodyTypeService)
        {
            _bodyTypeService = bodyTypeService;
        }

        [HttpPost("add")]
        public IActionResult Add(BodyType bodyType)
        {
            var result = _bodyTypeService.Add(bodyType);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(BodyType bodyType)
        {
            var result = _bodyTypeService.Delete(bodyType);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _bodyTypeService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int bodyTypeId)
        {
            var result = _bodyTypeService.GetById(bodyTypeId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(BodyType bodyType)
        {
            var result = _bodyTypeService.Update(bodyType);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
