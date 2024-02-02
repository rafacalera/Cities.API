using Cities.API.Infrastructure;
using Cities.API.Infrastructure.Models;
using Cities.API.Infrastructure.Repositories;
using Cities.API.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cities.API.Services
{
    public class CityServices:ICityServices
    {
        private readonly ICityRepository _cityRepository;

        public CityServices(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        private bool MissingFields(string name, string state)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(state))
                return true;

            return false;
        }

        public List<CityDTO> GetAll()
        {
            return _cityRepository.GetAll().Select(c => new CityDTO() { Id = c.Id, Name = c.Name, State = c.State }).ToList();
        }

        public CityDTO GetById(int id)
        {
            var city = _cityRepository.GetById(id);

            if (city == default)
            {
                return null;
            }

            return (new CityDTO() {Id = city.Id, Name = city.Name, State = city.State});
        }

        public CityDTO Add(string name, string state)
        {
            if (MissingFields(name, state)) return null;

            var city = new City(name, state);
            _cityRepository.Add(city);


            return new CityDTO() { Id = city.Id, Name = city.Name, State = city.State};
        }

        public CityDTO Update(int id, string name, string state)
        {
            var city = _cityRepository.GetById(id);

            if (MissingFields(name, state) || city == default) return null;

            city.Name = name;
            city.State = state;

            _cityRepository.Update(city);
            return new CityDTO() { Id = city.Id, Name = city.Name, State = city.State };
        }

        public bool Delete(int id)
        {
            var city = _cityRepository.GetById(id);
            if (city == default) return false;

            _cityRepository.Delete(city);
            return true;
        }


    }
}
