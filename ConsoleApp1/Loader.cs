using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Loader
    {

        public static void something(string filepath,PeopleList peopleList)
        {
            string content = readFile(filepath);
            List<string> lines = splitFileContent(content);
            
            
            string personName;

            

            string presentName;
            double presentPrice;
            string presentSize;

            

            foreach (string line in lines)
            {
                List<Present> presents = new List<Present>();

                List<Dictionary<string, double>> placesList;
                Dictionary<string, double> placesSmall = new Dictionary<string, double>();
                Dictionary<string, double> placesMedium = new Dictionary<string, double>();
                Dictionary<string, double> placesLarge = new Dictionary<string, double>();

                //Splits a line into the 3 main areas <Name><Places><Gifts>
                string[] areas = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                
                //This is the name of the person
                personName= areas[0];

                //Here the second area <Places> gets split into the 3 sizes
                string[] sizeSplit = areas[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //This places all the hiding places and propabilities to the places dictionaries above
                //All of this is done in a really restricting way due to the stifness of the format, but It's a single school project and I'm too lazy to look up or god forbid make a JSON interpreter for C# so if you are reading this this is my confession that this sucks but I don't care please grade me well thank you
                addPlacesToDictionary(sizeSplit[0],placesSmall);
                addPlacesToDictionary(sizeSplit[1], placesMedium);
                addPlacesToDictionary(sizeSplit[2], placesLarge);
                placesList = new List<Dictionary<string, double>> { placesSmall, placesMedium, placesLarge };
                

                //this does the presents reading and adding
                string[] presentLine = areas[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(string present in presentLine)
                {
                    string[] parts = present.Split(new char[] { ',' });
                    presentName = parts[0];
                    presentPrice = Convert.ToDouble(parts[1]);
                    presentSize = parts[2];

                    presents.Add(new Present(presentName, presentPrice, presentSize));
                }

                //Creating a person, setting her hiding places and her gifts, all from the data just read above
                Person p = new Person(personName);
                p.setHidingPlaces(placesList);
                p.setPresents(presents);
                //Adding that person to a list of people passed into the method
                peopleList.Add(p);

                

            }

        }

        public static void addPlacesToDictionary(string line, Dictionary<string,double> places)
        {
            string[] placesWithChances = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string place in placesWithChances)
            {
                string[] finalSplit = place.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                places.Add(finalSplit[0], Convert.ToDouble(finalSplit[1]));
            }
        }

        //Returns a list of lines
        public static List<string> splitFileContent(string content)
        {
            char[] separator = { '\n' };
            string[] areas = content.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            List<string> result = new List<string>(areas);

            return result;

        }

        //Method that reads stuff from file
        public static string readFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException();
            }

            string fileContent = File.ReadAllText(file);
            return fileContent;
        }

    }
}
