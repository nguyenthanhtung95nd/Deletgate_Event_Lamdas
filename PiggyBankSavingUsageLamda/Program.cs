using System;

namespace PiggyBankSavingUsageLamda
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

    public class Program
    {
        private static void Main(string[] args)
        {
            PiggyBank pb = new PiggyBank();

            pb.balanceChanged += (amount) => Console.WriteLine("Tiền đã tiết kiệm được {0}", amount);
            pb.balanceChanged += (amount) => { if (amount > 500.0m) Console.WriteLine("Đã tiết kiệm được {0} VND, Đủ tiền rồi mua PS5 thôi!", amount); };

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
