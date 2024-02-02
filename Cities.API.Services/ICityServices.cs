using Cities.API.Infrastructure.Models;
using Cities.API.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cities.API.Services
{
    public interface ICityServices
    {
        List<CityDTO> GetAll();
        CityDTO GetById(int id);
        CityDTO Add(string name, string state);
        CityDTO Update(int id, string name, string state);
        bool Delete(int id);

    }
}
