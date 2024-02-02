namespace Cities.API.Application.Models.ViewModels
{
    public class CityViewModel
    {
        public CityViewModel(string name, string state)
        {
            Name = name;
            State = state;
        }

        public string Name { get; set; }
        public string State { get; set; }
    }
}
