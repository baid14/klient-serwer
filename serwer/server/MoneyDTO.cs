using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class MoneyDTO:DTO
    {
        private double amount;

        public MoneyDTO() { }

        public void setAount(double amount)
        {
            this.amount = amount;
        }

        public double getAount()
        {
            return this.amount;
        }

    }
}
