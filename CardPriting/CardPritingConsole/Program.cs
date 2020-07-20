using CardPritingShare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardPritingConsole
{
    class Program
    {
        static CancellationTokenSource cts = new CancellationTokenSource();
        static void Main(string[] args)
        {
            cts = new CancellationTokenSource();
            Card card = new Card(cts.Token);
            List<string> needPrintCards = new List<string>()
            { "card1","card2","card3","card4","card5"};

            foreach (var item in needPrintCards)
            {

            }
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
