using System;
using SuperBank.BA;

namespace SuperBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var account = new BankAccount("Dan", 10000);

            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");


            account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
            Console.WriteLine(account.Balance);

            account.MakeDeposit(100, DateTime.Now, "friend paid me bakc");

            Console.WriteLine(account.Balance);


            var giftCard = new GiftCardAccount("gift card", 100, 50);
            giftCard.MakeWithdrawal(20, DateTime.Now, "get expensive coffee");
            giftCard.MakeWithdrawal(50, DateTime.Now, "buy groceries");

            giftCard.PerformMonthEndTransactions();

            //can make additional deposits:

            giftCard.MakeDeposit(27.50m, DateTime.Now, "add some additional spending");
            Console.WriteLine(giftCard.GetAccountHistory());



            var saving = new InterestEarningAccount("savings account", 10000);
            saving.MakeDeposit(750, DateTime.Now, "save some money");
            saving.MakeDeposit(1250, DateTime.Now, "add more savings");
            saving.MakeWithdrawal(250, DateTime.Now, "needs to pay monthly bills");

            saving.PerformMonthEndTransactions();
            Console.WriteLine(saving.GetAccountHistory());

            Console.WriteLine("Line of Credit");
            var lineOfCredit = new LineOfCreditAccount("line of credit", 0, 2000);
            // How much is too much to borrow?
            lineOfCredit.MakeWithdrawal(1000m, DateTime.Now, "Take out monthly advance");
            lineOfCredit.MakeDeposit(50m, DateTime.Now, "Pay back small amount");
            lineOfCredit.MakeWithdrawal(5000m, DateTime.Now, "Emergency funds for repairs");
            lineOfCredit.MakeDeposit(150m, DateTime.Now, "Partial restoration on repairs");
            lineOfCredit.PerformMonthEndTransactions();
            Console.WriteLine(lineOfCredit.GetAccountHistory());

            try
            {
                var invalidAccount = new BankAccount("invalid", -55);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught creating account with negative balance");
                Console.WriteLine(ex.ToString());
            }


            // Test for a negative balance.
            try
            {
                account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caught trying to overdraw");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
