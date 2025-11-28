using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Preethu.Phone.API.Models;
using Preethu.Phone.API.Repositories;
using Preethu.Phone.API.Services;

namespace Preethu.Phone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SmartPhoneController : ControllerBase
    {
        ISmartPhoneService smartPhoneService;
        ISmartPhoneRepository smartPhoneRepository;

        public SmartPhoneController(ISmartPhoneRepository smartPhoneRepository,
            ISmartPhoneService smartPhoneService)
        {
            this.smartPhoneRepository = smartPhoneRepository;
            this.smartPhoneService = smartPhoneService;
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
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(SmartPhone smartPhone)
        {
            string createStatus = smartPhoneService.Add(smartPhone);
            switch (createStatus)
            {
                case "success":
                    var smartPhoneAdded = smartPhoneRepository.GetByName(smartPhone.Name);
                    string msg = "Successfully created Smart Phone";
                    return Ok(msg);

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
            return Ok(msg );
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
        [HttpGet("search")]
        public IActionResult Search([FromQuery] SearchQuery filter)
        {
            var result = smartPhoneRepository.Search(filter);
            return Ok(result);
        }

    }
}
