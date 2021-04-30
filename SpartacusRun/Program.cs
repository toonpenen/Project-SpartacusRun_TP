using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpartacusRun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Druk <enter> om Jelle en Lotte te laten lopen");
            Console.ReadLine();
            Random rand = new Random();
            string[] personen = { "Jelle", "Lotte" };
            int[] meterAfgelegd = { 0, 0 };
            int[] huidigeSnelheid = { 0, 0 };
            int[] obstakelsAfstanden = { 100, 1100, 1500, 1900, 2500, 3200, 4000, 4600, 5200, 6000, 6900 };
            string[] obstakels = { "The wall", "Fallen Mikado", "Muddylicious", "Waterfest", "Terrilifying", "Tobogaaaan!", "Tyre mania", "Foooorzaa!", "Itsy bitsy spider", "De put", "Fear of the yeti!" };
            DateTime[] startTijden = { DateTime.Now, DateTime.Now };
            DateTime[] huidigeTijden = { DateTime.Now, DateTime.Now };
            bool[] finished = { false, false };
            int[] obstakelIndex = { -1, -1 };
            int teller = 0;
            bool running = true;
            const int totaleAfstand = 7000;
            while (running)
            {
                if (teller % 120 == 0)
                { // Om de twee minuten veranderen Jelle & Lotte van snelheid

                    huidigeSnelheid[0] = rand.Next(9, 14);//Jelle loopt minstens 9 per uur, maximum 14 per uur
                    huidigeSnelheid[1] = rand.Next(7, 12);//Lotte loopt minstens 7 per uur, maximum 12 per uur   
                }
                obstakelIndex[0] = -1;
                obstakelIndex[1] = -1;

                for (int i = 0; i < obstakelsAfstanden.Length; i++)
                {
                    if (meterAfgelegd[0] <= obstakelsAfstanden[i] && (meterAfgelegd[0] + KmUurNaarMeterSeconde(huidigeSnelheid[0])) > obstakelsAfstanden[i])
                    {
                        obstakelIndex[0] = i;
                        meterAfgelegd[0] = obstakelsAfstanden[i];
                    }
                    if (meterAfgelegd[1] <= obstakelsAfstanden[i] && (meterAfgelegd[1] + KmUurNaarMeterSeconde(huidigeSnelheid[1])) > obstakelsAfstanden[i])
                    {
                        obstakelIndex[1] = i;
                        meterAfgelegd[1] = obstakelsAfstanden[i];
                    }
                }
                if (obstakelIndex[0] == -1 && !finished[0])
                {
                    meterAfgelegd[0] += KmUurNaarMeterSeconde(huidigeSnelheid[0]);
                }
                else
                {
                    if (rand.Next(1000) >= 994)//0,6% kans om door te mogen lopen
                        meterAfgelegd[0] += 1;//1 meter erbij = obstakel voorbij
                }
                if (obstakelIndex[1] == -1 && !finished[1])
                {
                    meterAfgelegd[1] += KmUurNaarMeterSeconde(huidigeSnelheid[1]);
                }
                else
                {
                    if (rand.Next(100) >= 99)//1% kans om door te mogen lopen
                        meterAfgelegd[1] += 1;//1 meter erbij = obstakel voorbij
                }
                if (meterAfgelegd[0] > totaleAfstand)
                    finished[0] = true;
                if (meterAfgelegd[1] > totaleAfstand)
                    finished[1] = true;
                teller++;
                Thread.Sleep(100);
                Console.Clear();
                if (finished[0] && finished[1])
                    break;
                else
                    Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("*****{0} minuten, {1} seconden aan het lopen******", teller / 60, teller % 60);
                if (!finished[0])
                {
                    huidigeTijden[0] = huidigeTijden[0].AddSeconds(1);
                    if (meterAfgelegd[1] > meterAfgelegd[0])
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;
                    if (obstakelIndex[0] == -1)
                        Console.WriteLine("Jelle:\tsnelheid:{0}km/uur\n\tafstand: {1}m", huidigeSnelheid[0], meterAfgelegd[0]);
                    else
                    {
                        Console.WriteLine("Jelle zit momenteel vast aan obstakel {0}\n\tafstand: {1}m", obstakels[obstakelIndex[0]], meterAfgelegd[0]);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Jelle is gefinished!");
                }
                if (!finished[1])
                {
                    huidigeTijden[1] = huidigeTijden[1].AddSeconds(1);
                    if (meterAfgelegd[1] > meterAfgelegd[0])
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Red;

                    if (obstakelIndex[1] == -1)
                        Console.WriteLine("Lotte:\tsnelheid:{0}km/uur\n\tafstand: {1}m", huidigeSnelheid[1], meterAfgelegd[1]);
                    else
                    {
                        Console.WriteLine("Lotte zit momenteel vast aan obstakel {0}\n\tafstand: {1}m", obstakels[obstakelIndex[1]], meterAfgelegd[1]);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Lotte is gefinished!");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            if (huidigeTijden[0] < huidigeTijden[1])
                Console.WriteLine("\nJelle wint de race!\n");
            else
                Console.WriteLine("\nLotte wint de race!\n");

            Console.WriteLine("Jelle:\n\tStarttijd:{0}\n\tTijd aankomst:{1}", startTijden[0], huidigeTijden[0]);
            Console.WriteLine("Lotte:\n\tStarttijd:{0}\n\tTijd aankomst:{1}", startTijden[1], huidigeTijden[1]);
        }
        static int KmUurNaarMeterSeconde(int snelheid)
        {
            return snelheid * 1000 / 3600;
        }
    }
}
