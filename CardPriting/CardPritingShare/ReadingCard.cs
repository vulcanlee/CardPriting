using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardPritingShare
{
    public class ReadingCard
    {
        public object locker = new object();
        private Task ReadingTask;
        CancellationToken token;
        private ReadingActionEnum ReadingAction { get; set; }
        public event EventHandler CardStatusChange;

        public void Init()
        {
            ReadingTask = Task.Factory.StartNew(async () =>
            {
                await Reading();
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private async Task Reading()
        {
            while (true)
            {
                await Task.Delay(500);
                if (ReadingAction == ReadingActionEnum.NeedRead)
                {
                    ChangeStatus(ReadingActionEnum.READING);
                    Console.WriteLine("Reading Card");
                    await Task.Delay(3000);
                    Console.WriteLine("Reading Card has completed");
                    ChangeStatus(ReadingActionEnum.WAITING);
                }
            }
        }

        public void ChangeStatus(ReadingActionEnum newAction)
        {
            lock (locker)
            {
                ReadingCardEventArgs arg = new ReadingCardEventArgs()
                {
                    OldReadingAction = ReadingAction,
                    NewReadingAction = newAction
                };
                ReadingAction = newAction;
                CardStatusChange?.Invoke(this, arg);
            }
        }
    }
    public enum ReadingActionEnum
    {
        WAITING,
        NeedRead,
        READING,
        ERROR,
    }
    public class ReadingCardEventArgs : EventArgs
    {
        public ReadingActionEnum OldReadingAction { get; set; }
        public ReadingActionEnum NewReadingAction { get; set; }
    }
}
