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
    public class ManufacturerController : ControllerBase
    {
        IManufacturerRepository _manufacturerRepository;

        public ManufacturerController(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var manufacturers = _manufacturerRepository.GetManufacturers();
            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var manufacturer = _manufacturerRepository.GetById(id);
            if (manufacturer == null)
            {
                string msg1 = "Manufacturer doesn't exist!";
                return NotFound(msg1);
            }
            string msg2 = "Manufacturer listed Successfully";
            return Ok(new {manufacturer,msg2 });
        }

        [HttpPost]        
        public IActionResult Add(Manufacturer manufacturer)
        {
            var isCreated = _manufacturerRepository.CreateManufacturer(manufacturer);
            if (!isCreated)
            {
                return Conflict(new { error = $"A Manufacturer with the name '{manufacturer.Name}' already exists." });
            }
            var manufacturerAdded = _manufacturerRepository.GetById(manufacturer.MId);
            string msg = "Successfully created manufacturer";
            return Ok(new {manufacturerAdded,msg });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                string error = "Manufacturer doesnt exist";
                return BadRequest(error);
            }
            var isUpdated = _manufacturerRepository.Update(id, manufacturer);
            if (!isUpdated)
            {
                return NotFound($"No manufacturer of Id {id} found!");
            }
            var manufacturerUpdated = _manufacturerRepository.GetById(id);
            string msg = $"Details of manufacturer Id : {id} updated";
            return Ok(new { manufacturerUpdated, msg });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingmanufacturer = _manufacturerRepository.GetById(id);
            if (existingmanufacturer == null)
            {
                return NotFound("No such manufacturer exists!");
            }

            _manufacturerRepository.Delete(id);
            return Ok($"Manufacturer {id} deleted successfully!");
        }
    }
}
