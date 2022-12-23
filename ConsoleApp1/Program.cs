namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Dictionary<string,double> hidingPlacesSmall= new Dictionary<string,double>();
            hidingPlacesSmall.Add("Fireplace", 0.125);
            hidingPlacesSmall.Add("Tree", 0.250);
            hidingPlacesSmall.Add("Table", 0.500);

            Dictionary<string, double> hidingPlacesMedium = new Dictionary<string, double>();
            hidingPlacesMedium.Add("Fireplace", 0.2);
            hidingPlacesMedium.Add("Tree", 0.4);
            hidingPlacesMedium.Add("Table", 0.6);

            Dictionary<string, double> hidingPlacesLarge = new Dictionary<string, double>();
            hidingPlacesLarge.Add("Fireplace", 0.3);
            hidingPlacesLarge.Add("Tree", 0.6);
            hidingPlacesLarge.Add("Table", 0.9);
            hidingPlacesLarge.Add("Chair", 0.3);

            List<Dictionary<string, double>> list = new List<Dictionary<string, double>>();
            list.Add(hidingPlacesSmall);
            list.Add(hidingPlacesMedium);
            list.Add(hidingPlacesLarge);

            Person p = new Person("Fred");
            p.setHidingPlaces(list);

            p.presents.Add(new Present("Box", 6969, "small"));
            p.presents.Add(new Present("Bike", 21, "small"));
            p.presents.Add(new Present("Dog", 100, "small"));
            p.presents.Add(new Present("cat", 100, "small"));
            p.presents.Add(new Present("Tiger", 100, "large"));

            
            PeopleList pl = new PeopleList();
            pl.Add(p);
            
            pl.calculateHidingPlaces();

            Console.WriteLine("--------------------------------------");
            PeopleList pl2 = new PeopleList();
            Loader.something("../../../file.txt", pl2);
            pl2.calculateHidingPlaces();
        }
    }
}