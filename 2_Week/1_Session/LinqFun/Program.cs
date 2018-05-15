using System;
using System.Collections.Generic;
using System.Linq;
namespace LinqFun
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = {834,2,-23,2,103,-5,42,-1,27};

            int min = numbers.Min();


            int[] justEvens = numbers.Where(num => num%2==0).ToArray();

            List<string> names = new List<string>
            {
                "Sharon",
                "Charlie",
                "Barbara",
                "Molly",
                "Ashton",
                "Marcellus",
                "Molly",
                "Bob"
            };

            string minName = names.Min();
            // Todo: Find Bob


            
            List<City> cities = Place.GetCities();
            List<State> states = Place.GetStates();

            // Todo: get largest city
            City largest = cities.FirstOrDefault(city => city.Population == cities.Max(c => c.Population));
            // Todo: calculate population of California

            int CAPopulation = cities
                .Where(c => c.StateCode == "CA")
                .Sum(c => c.Population);
            
            // ["Seattle, Washington", ...]

            string[] cityFullName = 
                // initial collection
                cities
                // collection to join
                .Join(states, 
                // how to join city to state
                (c => c.StateCode), 
                // hoe to join state to city
                (s => s.StateCode), 
                // how we want results
                (joinedCity, joinedState) => {
                    return $"{joinedCity.Name}, {joinedState.Name}";
                }).ToArray();


        }
    }
}
