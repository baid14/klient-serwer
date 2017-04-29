using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class Account:IOperations
    {
        private double balance=0.0;

        public Account(double b)
        {
            balance = b;
        }

        public Account() : this(0) { }
/*
        public double Balance
        {
            get { return balance; }
            set { this.balance = value; }
        }
*/
        public void Withdraw(double amount)
        {
            if (amount > 0)
            {
                if ((balance-amount) >= -1000 )
                {
                    balance = balance - amount;
                }
                else
                {
                    throw new ArgumentException("balance<-1000");
                }
            }
            else
            {
                throw new ArgumentException("amount<=0");
            }
        }

        public void Deposit(double amount)
        {
            if(amount>0)
            {
                balance = balance + amount;
            }
            else
            {
                throw new ArgumentException("amount<=0");
            }
        }

        public double GetBalance()
        {
            return balance;
        }


    }
}
