using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cities.API.ConsoleApp.Models
{
    public class City
    {
        public City(string name, string state)
        {
            Name = name;
            State = state;
        }

        public string Name { get; set; }
        public string State { get; set; }
    }
}
