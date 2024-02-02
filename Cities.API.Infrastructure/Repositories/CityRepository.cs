using Cities.API.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cities.API.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly CityContext _context;

        public CityRepository(CityContext context)
        {
            _context = context;
        }

        public List<City> GetAll()
        {
            return _context.City.ToList();
        }

        public City GetById(int id)
        {
            return _context.City.FirstOrDefault(c => c.Id == id);
        }

        public void Add(City city)
        {
            _context.Add(city);
            _context.SaveChanges();
        }

        public void Update(City city)
        {
            _context.City.Update(city);
            _context.SaveChanges();
        }

        public void Delete(City city)
        {
            _context.City.Remove(city);
            _context.SaveChanges();
        }

        
    }
}
