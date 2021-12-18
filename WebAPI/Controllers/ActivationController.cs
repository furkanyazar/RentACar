using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivationController : ControllerBase
    {
        private IActivationService _activationService;

        public ActivationController(IActivationService activationService)
        {
            _activationService = activationService;
        }

        [HttpPost("add")]
        public IActionResult Add(Activation activation)
        {
            var result = _activationService.Add(activation);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Activation activation)
        {
            var result = _activationService.Delete(activation);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _activationService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int activationId)
        {
            var result = _activationService.GetById(activationId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _activationService.GetByUserId(userId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Activation activation)
        {
            var result = _activationService.Update(activation);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("setisactivatedforcustomer")]
        public IActionResult SetIsActivatedForCustomer(string email, string activationCode)
        {
            var result = _activationService.SetIsActivatedForCustomer(email, activationCode);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("setisactivatedforcompany")]
        public IActionResult SetIsActivatedForCompany(int userId, bool isActivated)
        {
            var result = _activationService.SetIsActivatedForCompany(userId, isActivated);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
