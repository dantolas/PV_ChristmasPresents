using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Person
    {

        public string name { get; private set; }
        //Map of key:size; value:map of key:hiding place;value:chance of finding the gift
        Dictionary<string, Dictionary<string, double>> hidingPlaces;
        //List darku ktere osoba ma dostat
        public List<Present> presents;

        //
        public Person(string name)
        {
            this.name = name;
            hidingPlaces = new Dictionary<string, Dictionary<string, double>>();
            presents = new List<Present>();
        }

        



        
        

        public void setPresents(List<Present> list)
        {
            this.presents = list;
        }

        //Do metody vstupuje list presne 3 map V PRESNEM PORADI!!! small,medium,large: key=skrys,val=pravdepodobnost nalezeni
        public void setHidingPlaces(List<Dictionary<string, double>> hidingPlaces)
        {
            if (hidingPlaces.Count() != 3)
            {

                throw new ArgumentOutOfRangeException("There can only by 3 different sets of hidingPlaces and values, each for one gift size.");
            }
            hidingPlaces.ForEach(dic => { Console.WriteLine(dic.Keys.First()); });
            this.hidingPlaces.Add("small", hidingPlaces.ElementAt(0));
            this.hidingPlaces.Add("medium", hidingPlaces.ElementAt(1));
            this.hidingPlaces.Add("large", hidingPlaces.ElementAt(2));
            Console.WriteLine("Done here");

        }

        //Method that prints the best hiding place for each gift
        //Also deletes the best suggestion (first suggestion if there are more than one) from the map of hiding places so that the next gift has a spot for itself
        public void calculateHidingPlaces()
        {
            Console.WriteLine("-------------------------");
            List<string>? currentBestPlaces = new List<string>();
            foreach (Present darek in presents)
            {
                currentBestPlaces = getBestHidingPlace(darek);
                
                //If currentBestPlaces == null, it means there are no stashes to hide the gift left
                if (currentBestPlaces == null)
                {
                    Console.WriteLine(this.name + ": " + darek);
                    Console.WriteLine("   -There is no more room for this gift :( ");
                    Console.WriteLine();
                    continue;
                }
                string print = "";
                //Builds the print string to it's final form
                foreach (string s in currentBestPlaces)
                {
                    print += s +":"+hidingPlaces[darek.size][s]+" -- ";
                }
                Console.WriteLine(this.name+": "+darek);
                Console.WriteLine("   -Best place to hide gift:: "+print);
                Console.WriteLine();
                //Removes the best suggestion if the map of hiding places isn't empty
                if(hidingPlaces[darek.size].Count() != 0) hidingPlaces[darek.size].Remove(currentBestPlaces.ElementAt(0));

            }
            Console.WriteLine("-------------------------");
        }

        //Method that returns a list of the best hiding places for a gift put into it
        public List<string>? getBestHidingPlace(Present p)
        {
            double lowestChance = 1;
            List<string> bestPlaces = new List<string>();

            if(hidingPlaces[p.size].Count() == 0)
            {
                return null;
            }

            foreach (KeyValuePair<string, double> pair in hidingPlaces[p.size])
            {
                
                if (pair.Value == lowestChance)
                {
                    lowestChance = pair.Value;
                    bestPlaces.Add(pair.Key);
                    continue;
                }
                if(pair.Value < lowestChance)
                {
                    bestPlaces.Clear();
                    lowestChance = pair.Value;
                    bestPlaces.Add(pair.Key);
                }

            }
            
            return bestPlaces;
        }
    }
}
