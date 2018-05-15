using System.Collections.Generic;

namespace LinqFun
{
    public class City
    {
        public string Name {get;set;}
        public string StateCode;
        public int Population;
        public City(string name, string stateCode, int pop)
        {
            Name = name;
            StateCode = stateCode;
            Population = pop;
        }
    }
    public class State
    {
        public string Name {get;set;}
        public string StateCode;
        public State(string name, string stateCode)
        {
            Name = name;
            StateCode = stateCode;
        }

    }
    public class Place
    {
        public static List<State> GetStates()
        {
            return new List<State>()
            {
                new State("Washington", "WA"),
                new State("California", "CA"),
                new State("Texas", "TX")
            };

        }
        public static List<City> GetCities()
        {
            return new List<City>()
            {
                new City("Seattle", "WA", 704542),
                new City("Tacoma", "WA", 211277),
                new City("Hoquiam", "WA", 8434),
                new City("Houston", "TX", 2303000),
                new City("Odessa", "TX", 117871),
                new City("Corpus Christi", "TX", 325733),
                new City("Los Angeles", "CA", 3976000),
                new City("Santa Cruz", "CA", 64465),
                new City("Barstow", "CA", 23835),
                new City("Bend", "OR", 13835)
            };
        }
    }
}
