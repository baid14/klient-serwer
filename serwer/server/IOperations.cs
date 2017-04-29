using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    interface IOperations
    {
        void Withdraw(double amount);
        void Deposit(double amount);
        double GetBalance();
    }
}
