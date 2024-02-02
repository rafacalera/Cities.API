using Cities.API.Application.Models.ViewModels;
using Cities.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cities.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityServices _services;

        public CitiesController(ICityServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CityViewModel>), 200)]
        [ProducesResponseType(204)]
        public IActionResult GetAll()
        {
            var cities = _services.GetAll();

            if (cities.Count == 0)
                return NoContent();



            return Ok(cities.Select(c => new CityViewModel(c.Name, c.State)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CityViewModel), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var cityDto = _services.GetById(id);

            if (cityDto == null) return NotFound();

            return Ok(new CityViewModel(cityDto.Name, cityDto.State));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CityViewModel),201)]
        [ProducesResponseType(400)]
        public IActionResult Add(CityViewModel cityViewModel)
        {
            var cityDto = _services.Add(cityViewModel.Name, cityViewModel.State);

            if (cityDto == null) return BadRequest("Missing required fields");
            
            return Created($"api/v1/Cities/{cityDto.Id}", new CityViewModel(cityDto.Name, cityDto.State));

        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CityViewModel), 200)]
        [ProducesResponseType(400)]
        public IActionResult Update(int id, CityViewModel cityViewModel)
        {
            var cityDto = _services.Update(id, cityViewModel.Name, cityViewModel.State);

            if (cityDto == null) return BadRequest();

            return Ok(new CityViewModel(cityDto.Name, cityDto.State));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            bool sucess = _services.Delete(id);
            if (!sucess) return NotFound();
            return Ok();
        }
    }
}
