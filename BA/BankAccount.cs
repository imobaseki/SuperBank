using System;
using System.Collections.Generic;

namespace SuperBank.BA
{
    public class BankAccount
    {

        private List<Transaction> allTransaction = new List<Transaction>();

        private readonly decimal minimumBalance;

        public virtual void PerformMonthEndTransactions() { }

        public string Number { get;  }


        public string Owner { get; set; }

        public decimal Balance {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransaction)
                {
                    balance += item.Amount;
                }

                return balance;
            }
            

                }

        private static int accountNumberSeed = 1234567890;


        public BankAccount(string name, decimal initialBalance): this(name,initialBalance,0)
        {
            this.Owner = name;
            
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

      
        public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            this.Owner = name;
            this.minimumBalance = minimumBalance;

            if(initialBalance > 0)
            {
                MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
            }
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if(amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amout of deposit must be postive");
            }

            var deposit = new Transaction(amount, date, note);
            allTransaction.Add(deposit);
        }


        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if(amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "AMount of withdrawal must be postive");
            }


            var overdraftTransaction = CheckWithdrawalLimit(Balance - amount < minimumBalance);

          
 
            var withdrawal = new Transaction(-amount, date, note);

            allTransaction.Add(withdrawal);
            if(overdraftTransaction != null)
            {
                allTransaction.Add(overdraftTransaction);
            }

        }



        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
                throw new InvalidOperationException("Not sufficient funds for withdrawal");
            }
            else
            {
                return default;
            }
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;

            report.AppendLine("Date\t\tAmount\tBalance\tNote");

            foreach (var item in allTransaction)
            {
                balance += item.Amount;
                report.AppendLine($"{ item.Date.ToShortDateString()}\t{ item.Amount}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }
    }


}
