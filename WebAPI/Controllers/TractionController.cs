using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TractionController : ControllerBase
    {
        private ITractionService _tractionService;

        public TractionController(ITractionService tractionService)
        {
            _tractionService = tractionService;
        }

        [HttpPost("add")]
        public IActionResult Add(Traction traction)
        {
            var result = _tractionService.Add(traction);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Traction traction)
        {
            var result = _tractionService.Delete(traction);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _tractionService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int tractionId)
        {
            var result = _tractionService.GetById(tractionId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Traction traction)
        {
            var result = _tractionService.Update(traction);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
