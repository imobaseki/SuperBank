using System;
using System.Security.Cryptography.X509Certificates;

namespace SuperBank.BA
{
    public class InterestEarningAccount : BankAccount
    {
        public InterestEarningAccount( string name, decimal initialBalance) :base(name, initialBalance)
        {


           
        }


        public override void PerformMonthEndTransactions()
        {
            if(Balance > 500m)
            {
                var interest = Balance * 0.05m;

                MakeDeposit(interest, DateTime.Now, "Apply monthly interest");
            }
        }
    }
}
