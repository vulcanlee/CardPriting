using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardPritingShare
{
    public class Card
    {
        CancellationToken token;
        PrinterStatus printerStatus = new PrinterStatus();
        public string Result { get; set; } = "OK";
        public Card(CancellationToken token)
        {
            this.token = token;
            printerStatus.PrinterStatusEvent += (s,e)=>
            {
                PrinterStatus status = s as PrinterStatus;
                Result = status.Result;
            };
        }
        public async Task Print(string info)
        {
        }
    }
}
