using Cities.API.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cities.API.Infrastructure.Repositories
{
    public interface ICityRepository
    {
        List<City> GetAll();
        City GetById(int id);
        void Add(City city);
        void Update(City city);
        public void Delete(City city);
    }
}
