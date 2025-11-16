using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Preethu.Phone.API.Models;
using Preethu.Phone.API.Repositories;

namespace Preethu.Phone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SmartPhoneController : ControllerBase
    {
        ISmartPhoneRepository smartPhoneRepository;

        public SmartPhoneController(ISmartPhoneRepository smartPhoneRepository)
        {
            this.smartPhoneRepository = smartPhoneRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var smartPhones = smartPhoneRepository.GetAll();
            var result = smartPhones.Select(s => new
            {
                SmartPhoneId = s.Id,
                SmartPhoneName = s.Name,
                Description = s.Description,
                Price = s.Price,
                Manufacturer = s.Manufacturer?.Name,
                Storage = s.Specification?.Storage,
                Operating_System = s.Specification?.OS,
                RAM = s.Specification?.RAM,
                Processor = s.Specification?.Processor
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var smartPhone = smartPhoneRepository.GetById(id);
            if (smartPhone == null)
            {
                string msg1 = "Smart Phone doesn't exist!";
                return NotFound(msg1);
            }
            var result = new
            {
                SmartPhoneId = smartPhone.Id,
                SmartPhoneName = smartPhone.Name,
                Description = smartPhone.Description,
                Price = smartPhone.Price,
                Manufacturer = smartPhone.Manufacturer?.Name,
                Storage = smartPhone.Specification?.Storage,
                Operating_System = smartPhone.Specification?.OS,
                RAM = smartPhone.Specification?.RAM,
                Processor = smartPhone.Specification?.Processor
            };
            string msg2 = "Smart Phone listed Successfully";
            return Ok(new {result, msg2 });
        }

        [HttpPost]
        public IActionResult Add(SmartPhone smartPhone)
        {
            string createStatus = smartPhoneRepository.Create(smartPhone);
            switch (createStatus)
            {
                case "success":
                    var smartPhoneAdded = smartPhoneRepository.GetById(smartPhone.Id);
                    string msg = "Successfully created Smart Phone";
                    return Ok(new { smartPhoneAdded, msg });

                case "duplicate name":
                    return BadRequest(new { message = $"A smartphone with the name '{smartPhone.Name}' already exists." });

                case "invalid manufacturer":
                    return BadRequest(new { message = $"Invalid Manufacturer: ID {smartPhone.MId} does not exist." });

                case "invalid specification":
                    return BadRequest(new { message = $"Invalid Specification: ID {smartPhone.SpecId} does not exist." });

                default:
                    return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, SmartPhone smartPhone)
        {
            if (smartPhone == null)
            {
                string error = "Smart Phone doesnt exist";
                return BadRequest(error);
            }
            var isUpdated = smartPhoneRepository.Update(id, smartPhone);
            if (!isUpdated)
            {
                return NotFound($"No Smart Phone of Id {id} found!");
            }
            var smartPhoneUpdated = smartPhoneRepository.GetById(id);
            string msg = $"Details of Smart Phone Id : {id} updated";
            return Ok(new { smartPhoneUpdated, msg });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingsmartPhone = smartPhoneRepository.GetById(id);
            if (existingsmartPhone == null)
            {
                return NotFound("No such Smart Phone exists!");
            }

            smartPhoneRepository.Delete(id);
            return Ok($"Smart Phone {id} deleted successfully!");
        }
        [HttpPost("Manufacturer")]
        public IActionResult GetByManufacturer([FromBody] string name)
        {
            var result = smartPhoneRepository.GetByManufacturer(name);
            string msg = $"All smartphones made by the manufacturer {name}";
            return Ok(new { msg, result });
        }
        [HttpPost("Specification")]
        public IActionResult GetBySpecs([FromBody] SearchQuery searchQuery)
        {
            var result = smartPhoneRepository.GetBySpecs(searchQuery);
            if (result == null)
            {
                return NotFound("There is no match for smart phones with the specs");
            }
            else
            {
                return Ok(new { result });
            }
        }
    }
}
