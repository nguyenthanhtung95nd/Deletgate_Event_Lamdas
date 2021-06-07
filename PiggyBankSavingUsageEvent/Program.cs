using System;

namespace PiggyBankSavingUsageEvent
{
    // Khai báo 1 delegate
    public delegate void BalanceEventHandler(decimal theValue);

    internal class PiggyBank
    {
        private decimal _bankBalance;

        // Khai báo 1 event handler
        public event BalanceEventHandler balanceChanged;

        public decimal TheBalance
        {
            set
            {
                _bankBalance = value;
                // when the value changes, fire the event
                balanceChanged(value);
            }
            get
            {
                return _bankBalance;
            }
        }
    }

    internal class BalanceLogger
    {
        public void BalanceLog(decimal amount)
        {
            Console.WriteLine("Tiền đã tiết kiệm được {0}", amount);
        }
    }

    internal class BalanceWatcher
    {
        public void BalanceWatch(decimal amount)
        {
            if (amount > 500.0m)
                Console.WriteLine("Đã tiết kiệm được {0} VND, Đủ tiền rồi mua PS5 thôi!", amount);
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            PiggyBank pb = new PiggyBank();
            BalanceLogger bl = new BalanceLogger();
            BalanceWatcher bw = new BalanceWatcher();

            // Event chaning
            pb.balanceChanged += bl.BalanceLog;
            pb.balanceChanged += bw.BalanceWatch;

            string theStr;
            do
            {
                Console.WriteLine("Bạn muốn gửi tiết kiệm bao nhiêu?");

                theStr = Console.ReadLine();
                if (!theStr.Equals("exit"))
                {
                    decimal newVal = decimal.Parse(theStr);

                    pb.TheBalance += newVal;
                }
            } while (!theStr.Equals("exit"));
        }
    }
}