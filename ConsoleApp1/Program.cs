using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("█───█─▄▀▀─█───▄▀▀─▄▀▀▄─█▄─▄█─▄▀▀\r"+"\n█───█─█───█───█───█──█─█▀▄▀█─█──\r\n█───█─█▀▀─█───█───█──█─█─▀─█─█▀▀\r\n█▄█▄█─█───█───█───█──█─█───█─█──\r\n─▀─▀───▀▀──▀▀──▀▀──▀▀──▀───▀──▀▀\r\n");
            Console.WriteLine("   *    *  ()   *   *\r\n*        * /\\         *\r\n      *   /i\\\\    *  *\r\n    *     o/\\\\  *      *\r\n *       ///\\i\\    *\r\n     *   /*/o\\\\  *    *\r\n   *    /i//\\*\\      *\r\n        /o/*\\\\i\\   *\r\n  *    //i//o\\\\\\\\     *\r\n    * /*////\\\\\\\\i\\*\r\n *    //o//i\\\\*\\\\\\   *\r\n   * /i///*/\\\\\\\\\\o\\   *\r\n  *    *   ||     *\n"
                );
           

            Console.WriteLine("--------------------------------------");
            while (true)
            {
                string? input =null;
                Random random = new Random();
                //input check
                while (input == null)
                {
                    Console.WriteLine("Vyberte zda chcete:\n1)Zadavat rucne\n2)Nacitat ze souboru\n3)Predvest ukazku programu");
                    input = Console.ReadLine();
                    if (!Regex.IsMatch(input, @"^\d$")) { Console.WriteLine("Prosim zadejte cislo.\n"); }
                }
                
                int numberInput = Convert.ToInt32(input);
                
                //-------------------------------------------------------------------------
                //Ukazka programu s nejakymy default hodnoty
                if (numberInput == 3)
                {
                    PeopleList peopleList = new PeopleList();
                    string[] names = { "Fred", "Amy", "Pavlat" };
                    foreach (string s in names)
                    {
                        Person p = new Person(s);
                        //Creating presents for person
                        List<Present> presents = new List<Present>();
                        presents.Add(new Present("Bike", 10000, "large"));
                        presents.Add(new Present("Phone", 25000, "small"));
                        presents.Add(new Present("Socks", 100, "small"));
                        presents.Add(new Present("Plushie", 100, "medium"));
                        presents.Add(new Present("Lotion", 150, "medium"));
                        //Adding presents to person
                        p.setPresents(presents);
                        //Creating 3 sets of hiding places: <small><medium><large>
                        List<string> stashes = new List<string>() { "Attic", "Under the table", "Outside" };

                        Dictionary<string, double> small = new Dictionary<string, double>();
                        Dictionary<string, double> medium = new Dictionary<string, double>();
                        Dictionary<string, double> large = new Dictionary<string, double>();
                        //Adding all the hiding places into these 3 maps, with a randomly generated propability double
                        stashes.ForEach(stash => { small.Add(stash, random.NextDouble()); large.Add(stash, random.NextDouble()); medium.Add(stash, random.NextDouble()); });
                        //Setting hiding places
                        List<Dictionary<string, double>> list = new List<Dictionary<string, double>>();
                        list.Add(small);
                        list.Add(medium);
                        list.Add(large);
                        p.setHidingPlaces(list);
                        peopleList.Add(p);
                        continue;
                    }
                    peopleList.calculateHidingPlaces();
                }
                //-------------------------------------------------------------------------
                //Nacitani ze souboru
                if (numberInput == 2)
                {
                    bool repeat = true;
                    PeopleList peopleList = new PeopleList();
                    Console.WriteLine("Nacitani ze souboru-- doporucujeme zobrazit manual pokud delate poprve.");
                    do {

                        Console.WriteLine("1)=>Zadat cestu k souboru\n2)=>Zobrazit manual");
                        input = Console.ReadLine();
                        //input check
                        while (!Regex.IsMatch(input, @"^\d$"))
                        {
                            if (!Regex.IsMatch(input, @"^\d$")) { Console.WriteLine("Prosim zadejte cislo.\n"); }
                            input = Console.ReadLine();
                        }
                        numberInput = Convert.ToInt32(input);
                        if (numberInput == 1)
                        {
                            Console.WriteLine("Pracovni adresar programu:" + Directory.GetCurrentDirectory());
                            Console.Write("Prosim zadejte cestu k souboru.\n=>");
                            input = Console.ReadLine();
                            try
                            {
                                Loader.something(input, peopleList);
                                peopleList.calculateHidingPlaces();
                                
                            }
                            catch (FileNotFoundException e)
                            {
                                Console.WriteLine("Soubor nebyl nalezen.");
                                continue;
                            }
                            repeat = false;
                        }
                        if (numberInput == 2)
                        {
                            showLoadManual();
                        }

                    } while (repeat);

                    continue;
                }
                //-------------------------------------------------------------------------
                //Zadavani rucne
                if (numberInput == 1)
                {
                    PeopleList peoplelist = new PeopleList();
                    do {
                        Console.Write("Zadejte jmeno osoby\n=>");
                        Person p = new Person(Console.ReadLine());
                        //Creating small stashes
                        Dictionary<string, double> small = createStash(numberInput, input, random, "malé");
                        //Creating medium stashes
                        Dictionary<string, double> medium = createStash(numberInput, input, random, "střední");
                        //Creating large stashes
                        Dictionary<string, double> large = createStash(numberInput, input, random, "velké");
                        List<Dictionary<string, double>> stashList = new List<Dictionary<string, double>>() { small, medium, large };
                        p.setHidingPlaces(stashList);
                        List<Present> presentList = createPresentsList(input);
                        p.setPresents(presentList);
                        peoplelist.Add(p);
                        Console.WriteLine("Osoba pridana\n1)Vytvorit dalsi osobu\n2)Finish");
                        input = Console.ReadLine();
                        //input check
                        while (!Regex.IsMatch(input, @"^\d$"))
                        {
                            if (!Regex.IsMatch(input, @"^\d$")) { Console.WriteLine("Prosim zadejte cislo.\n"); }
                            input = Console.ReadLine();
                        }

                    } while (Convert.ToInt32(input) == 1);
                    
                    
                    
                    peoplelist.calculateHidingPlaces();

                }

            }
            Console.WriteLine("Vyberte zda chcete:\n1)Zadavat rucne\n2)Nacitat ze souboru\n3)Predvest ukazku programu");
            PeopleList pl2 = new PeopleList();
            Loader.something("../../../file.txt", pl2);
            pl2.calculateHidingPlaces();
        }

        public static void showLoadManual()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Soubor z ktereho chcete nacitat by mel byt textovy, a text v nem MUSI!!! byt v presnem formatu. Priklad spravneho textoveho souboru => 'file.txt'");
            Console.WriteLine("Format textu:");
            Console.WriteLine("Kazdy radek je jedna osoba se skrysemi a darky.\nKazdy radek je rozdelen do 3 oblasti rozdelenych ';'");
            Console.WriteLine("V prvni oblasti je jmeno osoby. V druhe oblasti jsou skryse a pravdepodobnosti nalezeni. Ve treti oblasti jsou darky.\n---");
            Console.WriteLine("Priklad:\n| Cely radek | =>  Fred; fireplace:0.4456,table:0.4456,peen:0.2 fireplace:0.4456,table:0.4456,stairs:0.2 fireplace:0.3,table:0.3,stairs:0.2; Box,200,small wand,400,medium hand,1.34,medium leg,1,large hand,1.25554545454545,medium," +
                "\n--- \n| Oblast 1 | =>  Fred\n--");
            Console.WriteLine("| Oblast 2 | => fireplace:0.4456,table:0.4456,peen:0.2 fireplace:0.4456,table:0.4456,stairs:0.2 fireplace:0.3,table:0.3,stairs:0.2\n--");
            Console.WriteLine("| Oblast 3 | => Box,200,small wand,400,medium hand,1.255545,medium leg,1,large hand,1.34,medium,\n--");
            Console.WriteLine("Skryse v oblasti 2 jsou rozdeleny do 3 velikosti oddelenych mezerou, a jednotlive skryse se zapisuji <jmeno>:<sance nalezeni> a jsou oddeleny ',");
            Console.WriteLine("Priklad zapisu: (Male skryse)Za krbem:0.5,Pod schody:0.4 MEZERA (Stredni skryse)Za krbem:0.5,Pod schody:0.4 MEZERA (Velke skryse)Za krbem:0.5,Pod schody:0.4");
            Console.WriteLine("Vzdy musi byt v kazde velikosti skrysi alespon jedna skrys, i kdyz nebude pouzita.\n--");
            Console.WriteLine("Darky vo oblasti 3 se zapisuji <nazev>,<cena>,<velikost>MEZERA<nazev>,<cena>,<velikost>...");
            Console.WriteLine("!!!Velikost MUSI byt zapsana jako jedna z moznosti: small medium large");
            Console.WriteLine("!!!Pokud velikost bude zapsana jinak, program nebude fungovat\n--");
            Console.WriteLine("!!!Na konci kazdeho radku musi byt ',',jinak program nebude fungovat. (Some really strange bug happens when you don't that I couldn't seem to fix)");
            
            Console.WriteLine("---------------------------------------");
        }

        public static List<Present> createPresentsList(string input) {

            List<Present> list = new List<Present>();
            Console.WriteLine("Nasleduje přiřazení dárků.");
            bool repeat = true;
            string name;
            double price;
            string size;
            do
            {
                //Present name
                Console.Write("Prosim zadejte nazev darku\n=>");
                input = Console.ReadLine();
                name = input;
                
                input = "";

                //Present price
                //input check
                while (!Regex.IsMatch(input, @"-?\d+(?:\.\d+)?"))
                {
                    Console.Write("Prosim zadejte cenu darku.\n=>");
                    input = Console.ReadLine();
                    if (!Regex.IsMatch(input, @"-?\d+(?:\.\d+)?")) { Console.WriteLine("Prosim zadejte cislo.\n"); }
                }
                price = Convert.ToDouble(input);

                //Present size
                bool passed = false;
                while (!passed)
                {
                    Console.WriteLine("Prosim zadejte velikost darku. Musi byt presne jedna z nasledujicich: (small,medium,large)"); 
                    input = Console.ReadLine();
                    if (input.Equals("small") || input.Equals("medium") || input.Equals("large")) passed = true;
                    else Console.WriteLine("Nespravne zadana velikost.");
                }
                size = input;

                list.Add(new Present(name, price, size));

                Console.WriteLine("1) Zadat dalsi darek\n0) Nezadavat dalsi darek");
                input = Console.ReadLine();
                //input check
                while (!Regex.IsMatch(input, @"^\d$"))
                {
                    if (!Regex.IsMatch(input, @"^\d$")) { Console.WriteLine("Prosim zadejte cislo.\n"); }
                    input = Console.ReadLine();
                }
                if (Convert.ToInt32(input) != 1) repeat = false;

            } while (repeat);

            return list;

        }

        public static Dictionary<string,double> createStash(int numberInput,string input,Random random, string size) {

            Console.WriteLine("Nasleduje vytvareni pro skrysi pro !!!"+size+"!!! darky");
            Console.WriteLine("1) =>Pokud chcete pravdepodobnosti nalezeni zadat rucne");
            Console.WriteLine("2) =>Pokud chcete pravdepodobnosti nahodne generovat");
            input = Console.ReadLine();
            //input check
            while (!Regex.IsMatch(input, @"^\d$"))
            {
                if (!Regex.IsMatch(input, @"^\d$")) { Console.WriteLine("Prosim zadejte cislo.\n"); }
                input = Console.ReadLine();
            }
            numberInput = Convert.ToInt32(input);

            if (numberInput == 2)
            {
                return createHidingPlaces(true, random);

            }

            if (numberInput == 1)
            {
                return createHidingPlaces(false, random);
            }
            return null;


        }

        public static Dictionary<string, double> createHidingPlaces(bool generateChacnes, Random random)
        {
            Dictionary<string, double> returnMap = new Dictionary<string, double>();
            string input;
            bool repeat = true;
            //execute if chances of finding should be generated
            if (generateChacnes)
            {
                Console.WriteLine("Nyni budete zadavat skryse, pokud jste skoncili se zadavanim, zadejte 'exit'.\nZadavejte skryse:");
                do {
                    
                    input = Console.ReadLine();
                    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) continue;
                    returnMap.Add(input, random.NextDouble());
                    Console.WriteLine("--");
                    
                    


                } while (!input.Equals("exit",StringComparison.OrdinalIgnoreCase));
                return returnMap;
            }
            //execute if chances of finding should be manually added

            repeat = true;
            do
            {
                Console.WriteLine("Zadejte skrys:");
                input = Console.ReadLine();
                string place = input;
                //input check
                bool passed = false;
                while (!passed)
                {
                    Console.WriteLine("Zadejte cislo mezi 0-1 (Pr. 0.54533)");
                    input = Console.ReadLine();
                    passed = Regex.IsMatch(input, @"-?\d+(?:\.\d+)?");
                    if (!passed)
                    {
                        Console.WriteLine("Neni cislo.");
                        continue;
                    }

                    if(Convert.ToDouble(input) < 0 || Convert.ToDouble(input) > 1)
                    {
                        passed = false;
                        Console.WriteLine("Cislo neni v intervalu 0-1");
                    }
                }
                returnMap.Add(place, Convert.ToDouble(input));

                Console.WriteLine("1) Zadat dalsi skrys\n0) Nezadavat dalsi skrys");
                //input check
                while (!Regex.IsMatch(input, @"^\d$"))
                {
                    if (!Regex.IsMatch(input, @"^\d$")) { Console.WriteLine("Prosim zadejte cislo.\n"); }
                    input = Console.ReadLine();
                }
                if (Convert.ToInt32(input) != 1) repeat = false;


            } while (repeat);



            return null;
        }
    }

    
}