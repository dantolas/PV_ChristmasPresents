using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class PresentCalculator
    {

        /// <summary>
        /// Just the main method where all the method calls and code is used
        /// </summary>
        public static void mainMethod()
        {
            WelcomeTree();
            while (true)
            {
                
                string? input = null;
                Random random = new Random();
                //input check
                while (input == null || !Regex.IsMatch(input, @"^\d$"))
                {
                    Console.WriteLine("Vyberte zda chcete:\n1)Zadavat rucne\n2)Nacitat ze souboru\n3)Predvest ukazku programu");
                    input = Console.ReadLine();
                    if (!Regex.IsMatch(input, @"^\d$")) { Console.WriteLine("Prosim zadejte cislo.\n"); }
                }

                int numberInput = Convert.ToInt32(input);

                //-------------------------------------------------------------------------
                //Ukazka programu s nejakymi default hodnotami
                if (numberInput == 3)
                {
                    ProgramShowOff(random);
                }
                //-------------------------------------------------------------------------
                //Nacitani ze souboru
                if (numberInput == 2)
                {
                    ReadFromFile(input, numberInput);
                     continue;
                }
                //-------------------------------------------------------------------------
                //Zadavani rucne
                if (numberInput == 1)
                {
                    ManualEnter(input, numberInput, random);
                }

            }
            
        }

        /// <summary>
        /// Method that prints a neat welcome tree
        /// </summary>
        public static void WelcomeTree()
        {
            Console.WriteLine("█───█─▄▀▀─█───▄▀▀─▄▀▀▄─█▄─▄█─▄▀▀\r" + "\n█───█─█───█───█───█──█─█▀▄▀█─█──\r\n█───█─█▀▀─█───█───█──█─█─▀─█─█▀▀\r\n█▄█▄█─█───█───█───█──█─█───█─█──\r\n─▀─▀───▀▀──▀▀──▀▀──▀▀──▀───▀──▀▀\r\n");
            Console.WriteLine("   *    *  ()   *   *\r\n*        * /\\         *\r\n      *   /i\\\\    *  *\r\n    *     o/\\\\  *      *\r\n *       ///\\i\\    *\r\n     *   /*/o\\\\  *    *\r\n   *    /i//\\*\\      *\r\n        /o/*\\\\i\\   *\r\n  *    //i//o\\\\\\\\     *\r\n    * /*////\\\\\\\\i\\*\r\n *    //o//i\\\\*\\\\\\   *\r\n   * /i///*/\\\\\\\\\\o\\   *\r\n  *    *   ||     *\n"
                );
            Console.WriteLine("--------------------------------------");
        }

        /// <summary>
        ///Method that shows a manual for loading stuff from files
        /// </summary>
        public static void ShowLoadManual()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("********\nSoubor z ktereho chcete nacitat by mel byt textovy, a text v nem MUSI! byt v presnem formatu. Priklad spravneho textoveho souboru => 'christmasPresents\\ConsoleApp1\\file.txt'\n");
            Console.WriteLine("********\nFormat textu:");
            Console.WriteLine("Kazdy radek je jedna osoba se skrysemi a darky.\nKazdy radek je rozdelen do 3 oblasti rozdelenych ';'");
            Console.WriteLine("V prvni oblasti je jmeno osoby. V druhe oblasti jsou skryse a pravdepodobnosti nalezeni. Ve treti oblasti jsou darky.\n\n");
            Console.WriteLine("********\nPriklad:\n| Cely radek | =>  Fred; fireplace:0.4456,table:0.4456,attic:0.2 fireplace:0.4456,table:0.4456,stairs:0.2 fireplace:0.3,table:0.3,stairs:0.2; Box,200,small wand,400,medium hand,1.34,medium leg,1,large hand,1.25554545454545,medium," +
                "\n\n********\n| Oblast 1 | =>  Fred");
            Console.WriteLine("| Oblast 2 | => fireplace:0.4456,table:0.4456,peen:0.2 fireplace:0.4456,table:0.4456,stairs:0.2 fireplace:0.3,table:0.3,stairs:0.2");
            Console.WriteLine("| Oblast 3 | => Box,200,small wand,400,medium hand,1.255545,medium leg,1,large hand,1.34,medium,");
            Console.WriteLine("\nSkryse v oblasti 2 jsou rozdeleny do 3 velikosti oddelenych mezerou, a jednotlive skryse se zapisuji <jmeno>:<sance nalezeni> a jsou oddeleny ','");
            Console.WriteLine("********\n\nPriklad zapisu: (Male skryse)Za krbem:0.5,Pod schody:0.4 MEZERA (Stredni skryse)Za krbem:0.5,Pod schody:0.4 MEZERA (Velke skryse)Za krbem:0.5,Pod schody:0.4");
            Console.WriteLine("********\n\n!Vzdy musi byt v kazde velikosti skrysi alespon jedna skrys, i kdyz nebude pouzita.");
            Console.WriteLine("!Darky v oblasti 3 se zapisuji <nazev>,<cena>,<velikost>MEZERA<nazev>,<cena>,<velikost>...");
            Console.WriteLine("!Velikost MUSI byt zapsana jako jedna z moznosti: small medium large");
            Console.WriteLine("!Pokud velikost bude zapsana jinak, program nebude fungovat");
            Console.WriteLine("!Na konci kazdeho radku musi byt ',',jinak program nebude fungovat. (Some really strange bug happens when you don't that I couldn't seem to fix)");

            Console.WriteLine("---------------------------------------");
        }
        /// <summary>
        /// Method that creates a list of presents when manually entering presents
        /// </summary>
        /// <param name="input">For user input</param>
        /// <returns></returns>
        public static List<Present> CreatePresentsList(string input)
        {

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
        /// <summary>
        /// Method holding code and method calls to create stashes for all 3 gift sizes when manually entering
        /// </summary>
        /// <param name="numberInput">Console input converted to INT</param>
        /// <param name="input">Console user input</param>
        /// <param name="random">Random generator</param>
        /// <param name="size">Don't remember lol</param>
        /// <returns></returns>
        public static Dictionary<string, double> CreateStash(int numberInput, string input, Random random, string size)
        {

            Console.WriteLine("Nasleduje vytvareni pro skrysi pro !!!" + size + "!!! darky");
            Console.WriteLine("1) =>Pravdepodobnosti nalezeni zadat rucne");
            Console.WriteLine("2) =>Pravdepodobnosti nahodne generovat");
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
                return CreateHidingPlaces(true, random);

            }

            if (numberInput == 1)
            {
                return CreateHidingPlaces(false, random);
            }
            return null;


        }
        /// <summary>
        /// Method that creates hiding places when manually entering
        /// </summary>
        /// <param name="generateChacnes">Determines whether to generate chances</param>
        /// <param name="random">random generator</param>
        /// <returns></returns>
        public static Dictionary<string, double> CreateHidingPlaces(bool generateChacnes, Random random)
        {
            Dictionary<string, double> returnMap = new Dictionary<string, double>();
            string input;
            bool repeat = true;
            //execute if chances of finding should be generated
            if (generateChacnes)
            {
                Console.WriteLine("Nyni budete zadavat skryse, pokud jste skoncili se zadavanim, zadejte 'exit'.\nZadavejte skryse:");
                do
                {

                    input = Console.ReadLine();
                    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) continue;
                    returnMap.Add(input, random.NextDouble());
                    Console.WriteLine("--");




                } while (!input.Equals("exit", StringComparison.OrdinalIgnoreCase));
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

                    if (Convert.ToDouble(input) < 0 || Convert.ToDouble(input) > 1)
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


        /// <summary>
        /// Method containing code and method calls for manually entering all Presents, and creating stashes
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="numberInput">User input INT</param>
        /// <param name="random">random generator</param>
        public static void ManualEnter(string input, int numberInput,Random random)
        {
            PeopleList peoplelist = new PeopleList();
            do
            {
                Console.Write("Zadejte jmeno osoby\n=>");
                Person p = new Person(Console.ReadLine());
                //Creating small stashes
                Dictionary<string, double> small = CreateStash(numberInput, input, random, "malé");
                //Creating medium stashes
                Dictionary<string, double> medium = CreateStash(numberInput, input, random, "střední");
                //Creating large stashes
                Dictionary<string, double> large = CreateStash(numberInput, input, random, "velké");
                List<Dictionary<string, double>> stashList = new List<Dictionary<string, double>>() { small, medium, large };
                p.setHidingPlaces(stashList);
                List<Present> presentList = CreatePresentsList(input);
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
        /// <summary>
        /// Method containing code to read everything from a file
        /// </summary>
        /// <param name="input"></param>
        /// <param name="numberInput"></param>
        public static void ReadFromFile(string input,int numberInput)
        {
            bool repeat = true;
            PeopleList peopleList = new PeopleList();
            Console.WriteLine("Nacitani ze souboru-- doporucujeme zobrazit manual pokud delate poprve.");
            do
            {

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
                    Console.WriteLine("*Pracovni adresar programu:" + Directory.GetCurrentDirectory()+"\n*Testovaci soubor: ../../../file.txt");
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
                    ShowLoadManual();
                }

            } while (repeat);

        }
        /// <summary>
        /// Shows of the program with some default values
        /// </summary>
        /// <param name="random"></param>
        public static void ProgramShowOff(Random random)
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
            Console.WriteLine("Ukazku programu naleznete ve tride PresentCalculator.cs metoda ProgramShowOff");
        }

    }
}

