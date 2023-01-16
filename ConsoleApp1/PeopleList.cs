using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class PeopleList
    {
        public List<Person> peopleList;

        public PeopleList()
        {
            peopleList= new List<Person>();
        }

        public PeopleList(List<Person> peopleList)
        {
            this.peopleList = peopleList;
        }

        public void Add(Person person)
        {
            this.peopleList.Add(person);
        }

        public void CalculateHidingPlaces()
        {
            peopleList.ForEach(person => {
                person.CalculateHidingPlaces();
            });
        }
    }
}
