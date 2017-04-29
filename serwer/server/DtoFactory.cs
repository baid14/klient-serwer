using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class DtoFactory
    {
        public DTO getDTO(string name)
        {
            if (name == null)
            {
                return null;
            }
            if (name.Equals("MessageDTO"))
            {
                return new MessageDTO();

            }
            else if (name.Equals("UserDTO"))
            {
                return new UserDTO();

            }
            else if (name.Equals("MoneyDTO"))
            {
                return new MoneyDTO();
            }

            return null;
        }
    }
}
