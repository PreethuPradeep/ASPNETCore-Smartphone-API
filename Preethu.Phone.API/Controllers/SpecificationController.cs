using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Preethu.Phone.API.Models;
using Preethu.Phone.API.Repositories;

namespace Preethu.Phone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationController : ControllerBase
    {
        ISpecificationRepository specificationRepository;

        public SpecificationController(ISpecificationRepository specificationRepository)
        {
            this.specificationRepository = specificationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var specifications = specificationRepository.GetSpecifications();
            return Ok(specifications);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var specification = specificationRepository.GetById(id);
            if (specification == null)
            {
                string msg1 = "Smart Phone doesn't exist!";
                return NotFound(msg1);
            }
            string msg2 = "Smart Phone listed Successfully";
            return Ok(new { specification, msg2 });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(SmartPhoneSpec specification)
        {
            var isCreated = specificationRepository.Create(specification);
            if (!isCreated)
            {
                return Conflict(new { error = "A specification with the same Processor, RAM, Storage, and OS already exists." });
            }
            var specificationAdded = specificationRepository.GetById(specification.SpecId);
            string msg = "Successfully created Smart Phone specification";
            return Ok(new { specificationAdded, msg });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit(int id, SmartPhoneSpec specificationUpdate)
        {
            if (specificationUpdate == null)
            {
                string error = "Smart Phone specification doesnt exist";
                return BadRequest(error);
            }
            var isUpdated = specificationRepository.Update(id, specificationUpdate);
            if (!isUpdated)
            {
                return NotFound($"No Smart Phone specification of Id {id} found!");
            }
            var specification = specificationRepository.GetById(id);
            string msg = $"Details of Smart Phone specification Id : {id} updated";
            return Ok(new { specification, msg });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var existingSpecification = specificationRepository.GetById(id);
            if (existingSpecification == null)
            {
                return NotFound("No such Smart Phone specification exists!");
            }

            specificationRepository.Delete(id);
            return Ok($"Smart Phone specification {id} deleted successfully!");
        }
    }
}
