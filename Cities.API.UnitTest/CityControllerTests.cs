using Cities.API.Application.Models.ViewModels;
using Cities.API.Controllers;
using Cities.API.Infrastructure.Models;
using Cities.API.Services;
using Cities.API.Services.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Cities.API.UnitTest
{
    public class CityControllerTests
    {
        [Fact]
        public void GetAll_HasRegistrations_Ok()
        {
            // Arrange
            var mockServices = new Mock<ICityServices>();
            var cities = new List<CityDTO>
            {
            new CityDTO(){ Name = "City1", State = "State1" },
            new CityDTO() { Name = "City2", State = "State2" }
            };

            mockServices.Setup(s => s.GetAll()).Returns(cities);

            var controller = new CitiesController(mockServices.Object);


            var result = controller.GetAll();


            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var viewModelList = okResult.Value as IEnumerable<CityViewModel>;
            Assert.NotNull(viewModelList);

            var firstViewModel = viewModelList.FirstOrDefault();
            Assert.NotNull(firstViewModel);
            Assert.Equal("City1", firstViewModel.Name);
            Assert.Equal("State1", firstViewModel.State);
        }

        [Fact]
        public void GetAll_NoRegistrations_NoContent()
        {
            var mockServices = new Mock<ICityServices>();
            var cities = new List<CityDTO>();

            mockServices.Setup(s => s.GetAll()).Returns(cities);
            var controller = new CitiesController(mockServices.Object);


            var result = controller.GetAll();

            var noContentResult = result as NoContentResult;

            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public void GetById_FoundId_Ok()
        {
            var mockServices = new Mock<ICityServices>();
            var city = new CityDTO() { Id = 1, Name = "City1", State = "State1" };

            mockServices.Setup(s => s.GetById(city.Id)).Returns(city);
            var controller = new CitiesController(mockServices.Object);

            var result = controller.GetById(city.Id);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var cityViewModel = okResult.Value as CityViewModel;

            Assert.NotNull(cityViewModel);
            Assert.Equal("City1", cityViewModel.Name);
            Assert.Equal("State1", cityViewModel.State);
        }

        [Fact]
        public void GetById_NotFoundId_NotFound()
        {
            var mockServices = new Mock<ICityServices>();
            int id = 2;
            CityDTO cityDTO = null;


            mockServices.Setup(s => s.GetById(id)).Returns(cityDTO);
            var controller = new CitiesController(mockServices.Object);

            var result = controller.GetById(id);

            var notFound = result as NotFoundResult;

            Assert.NotNull(notFound);
            Assert.Equal(404, notFound.StatusCode);
        }

        [Fact]
        public void Add_IsValid_Created()
        {
            var mockServices = new Mock<ICityServices>();
            CityViewModel cityViewModel = new CityViewModel("City1", "State1");
            var cityDto = new CityDTO { Id = 1, Name = cityViewModel.Name, State = cityViewModel.State };

            mockServices.Setup(s => s.Add(cityViewModel.Name, cityViewModel.State)).Returns(cityDto);
            var controller = new CitiesController(mockServices.Object);

            var result = controller.Add(cityViewModel);

            var createdResult = result as CreatedResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);

            var resultCityViewModel = createdResult.Value as CityViewModel;
            Assert.NotNull(resultCityViewModel);
            Assert.Equal(cityViewModel.Name, resultCityViewModel.Name);
            Assert.Equal(cityViewModel.State, resultCityViewModel.State);

            var resultLocation = createdResult.Location;
            Assert.NotNull(resultLocation);
            Assert.Equal($"api/v1/Cities/{cityDto.Id}", resultLocation);
        }

        [Fact]
        public void Add_NotValid_BadRequest()
        {
            var mockServices = new Mock<ICityServices>();
            CityViewModel cityViewModel = new CityViewModel("City1", "");
            CityDTO cityDto = null;

            mockServices.Setup(s => s.Add(cityViewModel.Name, cityViewModel.State)).Returns(cityDto);
            var controller = new CitiesController(mockServices.Object);

            var result = controller.Add(cityViewModel);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_IsValid_Ok()
        {
            var mockServices = new Mock<ICityServices>();
            var id = 1;
            CityViewModel cityViewModel = new CityViewModel("City2", "State2");
            CityDTO cityDto = new CityDTO() { Id = id, Name = cityViewModel.Name, State = cityViewModel.State };

            mockServices.Setup(s => s.Update(id, cityViewModel.Name, cityViewModel.State)).Returns(cityDto);
            var controller = new CitiesController(mockServices.Object);

            var result = controller.Update(id, cityViewModel);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var resultCityViewModel = okResult.Value as CityViewModel;
            Assert.NotNull(resultCityViewModel);
            Assert.Equal(cityViewModel.Name, resultCityViewModel.Name);
            Assert.Equal(cityViewModel.State, resultCityViewModel.State);
        }

        [Fact]
        public void Update_NotValid_BadRequest()
        {
            var mockServices = new Mock<ICityServices>();
            var id = 1;
            CityViewModel cityViewModel = new CityViewModel("City2", "");
            CityDTO cityDto = null;

            mockServices.Setup(s => s.Update(id, cityViewModel.Name, cityViewModel.State)).Returns(cityDto);
            var controller = new CitiesController(mockServices.Object);

            var result = controller.Update(id, cityViewModel);

            var badRequestResult = result as BadRequestResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Delete_IsValid_Ok()
        {
            var mockServices = new Mock<ICityServices>();
            int id = 1;

            mockServices.Setup(s => s.Delete(id)).Returns(true);
            var controller = new CitiesController(mockServices.Object);

            var result = controller.Delete(id);

            var okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void Delete_NotValid_NotFound()
        {
            var mockServices = new Mock<ICityServices>();
            int id = 2;

            mockServices.Setup(s => s.Delete(id)).Returns(false);
            var controller = new CitiesController(mockServices.Object);

            var result = controller.Delete(id);
            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}