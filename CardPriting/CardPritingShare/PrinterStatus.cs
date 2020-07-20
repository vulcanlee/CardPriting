using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardPritingShare
{
    public class PrinterStatus
    {
        public object locker = new object();
        CancellationToken token;
        public static bool IsFailure = false;
        public event EventHandler<PrinterStatusEventArgs> PrinterStatusEvent;
        public string Result { get; set; } = "OK";
        public void GetStatus(string info)
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                        break;
                    await Task.Delay(500);
                    if (IsFailure)
                    {
                        Result = "Fail";
                    }
                    else
                    {
                        Result = "OK";
                    }
                    PrinterStatusEvent?.Invoke(this, null);
                }
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
        public class PrinterStatusEventArgs : EventArgs
        {
            public ReadingActionEnum OldReadingAction { get; set; }
            public ReadingActionEnum NewReadingAction { get; set; }
        }
    }
}
