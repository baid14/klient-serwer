using Serialisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public abstract class DTO : CustomSerialization
    {
        protected int operationType;

        public void setOperationType(int operationType)
        {
            this.operationType = operationType;
        }

        public int getOperationType()
        {
            return this.operationType;
        }
    }
}
