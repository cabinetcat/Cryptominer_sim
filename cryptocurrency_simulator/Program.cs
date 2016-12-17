using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace cryptocurrency_simulator
{
    class Program
    {
        static int currentpool;
        static double machinehashrate = 0;
        static int cash = 0;
        static int currentmachine = 0;
        static int[] poolnums = new int[5];
        static double[] ghznums = new double[5];
        static void Main(string[] args)
        {

            string readfromfile = System.IO.File.ReadAllText(@"C:\Temp\ccs_sid.txt");
            string getdatefromfile = readfromfile.Substring(0, 21);
            var start = DateTime.Now;
            var oldDate = DateTime.Parse(getdatefromfile);
            // check if the date contained in the SID is within 12 hours of the current time.
            if (start.Subtract(oldDate) >= TimeSpan.FromHours(12))
            {
                //generate new session id
                Console.WriteLine("New session ID generated!");
                sessionID();
            }

            else
            {
                //read from the SID to get & set the data

                string poolsfromfile = readfromfile.Substring(readfromfile.IndexOf("%") + 1, 9);
                string ghznumsfromfile = readfromfile.Substring(readfromfile.IndexOf("&") + 1, 21);
                poolnums = poolsfromfile.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                ghznums = ghznumsfromfile.Split(',').Select(n => Convert.ToDouble(n)).ToArray();


            }
            var logo = new[]
                {
                    @" _____ ___  ___ _____ ",
                    @"/  __ \|  \/  |/  ___|",
                    @"| /  \/| .  . |\ `--. ",
                    @"| |    | |\/| | `--. \",
                    @"| \__/\| |  | |/\__/ /",
                    @" \____/\_|  |_/\____/ "                  
                };
            foreach (string line in logo)
                Console.WriteLine(line);


            Console.WriteLine("Welcome to Cryptocurrency simulator");


            Console.WriteLine("1. New game{0}2. Load game", Environment.NewLine);
            switch (Console.ReadLine())
            {
                case "1":
                    cash = 180;
                    Console.Title = updatetitle( 0);
                    Console.Clear();
                    buymachine(cash);


                    break;
                case "2":
                    break;
            }

        }
        static string blockID()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {

                sb.Append(chars[rng.Next(0, 36)]);

            }
            return sb.ToString();
        }
        static void Status()
        {
            double currentpoolspeed = ghznums[currentpool];
            Console.WriteLine("Shares: {3} Mining in pool: {1} {0}Current block: {2} {0} 1. Refresh {0} 2. Exit", Environment.NewLine, poollist[poolnums[currentpool]], blockID(), finalshares);
            switch(Console.ReadLine())
            {
                    //hashrate = rate of shares/hr
                    //pool speed = chance of getting share(s)
                case "1":
                    {
                        sharefact();
                        updatetitle(0);
                        Console.Clear();
                        Status();


                    }
                    break;
                case "2":
                    {
                        Console.Clear();
                        mainmenu();
                    }
                    break;
            }
        }
        // this generates the session id, which stores several values.
        static string sessionID()
        {
            string[] fullstring = new string[3];
            fullstring[0] = "" + Convert.ToString(DateTime.Now);
            Random rrr = new Random();
            HashSet<int> numbers = new HashSet<int>();

            while (numbers.Count < 5)
            {
                numbers.Add(rrr.Next(9));
            }
            fullstring[1] = "%" + string.Join(",", numbers);
            fullstring[2] = "&";
            for (int i = 0; i < 5; i++)
            {

                double mint = RNG(i + 1) / 1000;
                fullstring[2] += Math.Truncate(100 * mint) / 100 + ",";
            }
            string FINAL = String.Join("|", fullstring);
            System.IO.File.WriteAllText(@"C:\Temp\ccs_sid.txt", FINAL);
            return FINAL;
        }
        static string updatetitle(int coins)
        {
            string title = ("Shares:" + finalshares + " Cash:" + cash + " Hashrate:" + machinehashrate + " Coins:" + coins);
            return title;
        }
        static DateTime dtnow1;
        static string[] poollist = { "Worldbit", "Dizio Netherlands", "Tenarch", "Digital Trade Foundation", "Globalsolution", "Iminers", "Coin Collective", "Mega Pool", "Line Miner Forums", "Resistance", };
        static void pools()
        {
            Console.WriteLine("Connect to a pool");
            Random rrr = new Random();

            for (int i = 0; i < 5; i++)
            {
                double mint = RNG(i + 1) / 1000;


                Console.WriteLine("{0}. {1}  {2}Ghz", i + 1, poollist[poolnums[i]], ghznums[i]);

            }
            String crl = Console.ReadLine();
            if (crl.Length > 1)
            {
                Console.WriteLine("Please pick a pool number...");
                Console.ReadKey();
                Console.Clear();
                pools();
            }
            else
            {
                dtnow1 = DateTime.Now;
                    
                currentpool = Convert.ToInt16(crl) - 1;
                Console.Clear();
                mainmenu();
            }
        }
        static double finalshares = 0;
        static void sharefact()
        {
            TimeSpan difference = DateTime.Now - dtnow1;

            
            int minutespassed = difference.Minutes;
            
            
            finalshares =   finalshares + minutespassed * machinehashrate - finalshares;
            
        }
        // my RNG 
       static char[] chars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        static Random rng = new Random();
        static double RNG(int num)
        {
            
            
            
                
                int between = rng.Next(000, 265);
                return (Double)between / num * 100.00;
            
            
            
                
            
        }
        enum machines { ARMMiner = 180, Delgado = 211, PHS32 = 286, ARMMinerII = 491, DigitalSuite = 1198 };

        static void buymachine(int cashamount)
        {
        Machinechoice:
            foreach (var value in Enum.GetValues(typeof(machines)))
            {
                if (cashamount >= (int)value)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine("{0}  ${1}", (machines)value, (int)value);
            }

            switch (Console.ReadLine())
            {
                case "1":
                    if (cash >= (int)machines.ARMMiner)
                    {
                        currentmachine = 1;
                        machinehashrate = 1.31;

                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds!");
                        Console.ReadKey();
                        Console.Clear();
                        goto Machinechoice;
                    }
                    break;
                case "2":
                    if (cash >= (int)machines.Delgado)
                    {
                        currentmachine = 2;
                        machinehashrate = 4.86;

                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds!");
                        Console.ReadKey();
                        Console.Clear();
                        goto Machinechoice;
                    }
                    break;
                case "3":
                    if (cash >= (int)machines.PHS32)
                    {
                        currentmachine = 3;
                        machinehashrate = 15.51;

                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds!");
                        Console.ReadKey();
                        Console.Clear();
                        goto Machinechoice;
                    }
                    break;
                case "4":
                    if (cash >= (int)machines.ARMMinerII)
                    {
                        currentmachine = 4;
                        machinehashrate = 47.46;

                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds!");
                        Console.ReadKey();
                        Console.Clear();
                        goto Machinechoice;
                    }
                    break;
                case "5":
                    if (cash >= (int)machines.DigitalSuite)
                    {
                        currentmachine = 5;
                        machinehashrate = 143.31;

                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds!");
                        Console.ReadKey();
                        Console.Clear();
                        goto Machinechoice;
                    }
                    break;


            }
            if (currentmachine > 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                mainmenu();
            }

            Console.ReadLine();
        }
        static void mainmenu()
        {
            Console.WriteLine("1. Select a mining pool{0}2. Buy a machine{0}3. Current mining status", Environment.NewLine);
            switch(Console.ReadLine())
            {
                    
                case "1":
                    {
                        Console.Clear();
                        pools();
                    }
               break;
                case "2":
               {
                   Console.Clear();
                   buymachine(cash);
               }
               break;
                case "3":
               {
                   Console.Clear();
                   Status();
               }
               break;
            }
        }
    }
}
  

    

