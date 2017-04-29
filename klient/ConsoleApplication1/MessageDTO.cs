using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class MessageDTO : DTO
    {
        string message;
        bool status;

        public MessageDTO(int type, string msg, bool status)
        {
            this.setOperationType(type);
            this.message = msg;
            this.status = status;
        }

        public MessageDTO() { }
        public void setMessage(string message)
        {
            this.message = message;
        }

        public string getMessage()
        {
            return this.message;
        }

        public bool getStatus()
        {
            return this.status;
        }

        public void setStatus(bool status)
        {
            this.status = status;
        }

    }
}
