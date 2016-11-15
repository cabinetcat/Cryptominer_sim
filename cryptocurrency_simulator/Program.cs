using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_simulator
{
    class Program
    {
        static int cash = 0;

        static int[] poolnums = new int[5];
        static int[] ghznums = new int[5];
        static void Main(string[] args)
        {

            string readfromfile = System.IO.File.ReadAllText(@"C:\Temp\ccs_sid.txt");
            string getdatefromfile = readfromfile.Substring(0,21);
            var start = DateTime.Now;
            var oldDate = DateTime.Parse(getdatefromfile);

            if (start.Subtract(oldDate) >= TimeSpan.FromHours(12))
            {
                Console.WriteLine(sessionID());
            }
            else
            {
                string poolsfromfile = readfromfile.Substring(readfromfile.IndexOf("%"), 10);
                for (int i = 0; i < 5; i++)
                {

                }
            }


            Console.WriteLine("Welcome to Cryptocurrency simulator");
            
            
            Console.WriteLine("1. New game{0}2. Load game", Environment.NewLine);
            switch (Console.ReadLine())
            {
                case "1":
                    cash = 180;
                    Console.Title = (updatetitle(0, cash, 0, 0));
                    Console.Clear();
                    pools(null);
                    
                    break;
                case "2":
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
                numbers.Add(rrr.Next(10));
            }
            fullstring[1] = "%" + string.Join(",", numbers);
            fullstring[2] = "&";
              for (int i = 0; i < 5; i++)   
              {
                  double mint = RNG(i + 1)/1000;
                  fullstring [2] += "/ " + Math.Truncate(100 * mint) / 100;
              }
             string FINAL = String.Join("|", fullstring);
             System.IO.File.WriteAllText(@"C:\Temp\ccs_sid.txt", FINAL);
             return FINAL;
            }
        static string updatetitle(int hashrate, int cash, int shares, int coins)
        {
            string title = ("Shares:" + shares + " Cash:" + cash + " Hashrate:" + hashrate + " Coins:" + coins);
            return title;
        }
        static string[] poollist = { "Worldbit", "Dizio Netherlands", "Tenarch", "Digital Trade Foundation", "Globalsolution", "Iminers", "Coin Collective", "Mega Pool", "Line Miner Forums", "Resistance", };
       static void pools(string args)
        {
            Console.WriteLine("Connect to a pool");
            Random rrr = new Random();
         
           for (int i = 0; i < 5; i++)
           {
               double mint = RNG(i + 1)/1000;
               
               
              Console.WriteLine("{0}. {1}  {2}Ghz", i+1, poollist[poolnums[i]], ghznums[i]);
           }
           
           Console.ReadLine();
        }
        static double RNG(int num)
       {
           Random rng = new Random();
           int between = rng.Next(000, 265);
           return (Double)between / num * 100.00;
            
       }
        enum machines { ARMMiner = 179, Delgado = 211, PHS32 = 286, ARMMinerII = 491, DigitalSuite = 1198 };
        
        static void buymachine(int cashamount)
        {
            
            foreach (var value in Enum.GetValues(typeof(machines)))
            {
                if (cashamount <= (int)value)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine( "{0}  ${1}", (machines)value, (int)value);
                
            }
            
            Console.ReadLine();
        }
    }
}
  

    

