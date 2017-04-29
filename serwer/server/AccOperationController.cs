using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class AccOperationController
    {
        private Account account;

        public AccOperationController(Account acc)
        {
            this.account = acc;
        }

        private DTO Withdraw(MoneyDTO moneyDTO)
        {
            MessageDTO messageDTO = null;
            double amount = moneyDTO.getAount();
            try
            {
                account.Deposit(amount);
                messageDTO = new MessageDTO(moneyDTO.getOperationType(), "Succesfully deposited " + amount + "$", true);
            }
            catch (ArgumentException ex)
            {
                messageDTO = new MessageDTO(moneyDTO.getOperationType(), ex.Message, true);
            }
            return messageDTO;
        }

        private DTO Deposit(MoneyDTO moneyDTO)
        {
             MessageDTO messageDTO = null;
             double amount = moneyDTO.getAount();
             try
             {
                if (account.GetBalance() >= amount)
                {
                    account.Withdraw(amount);
                    messageDTO = new MessageDTO(moneyDTO.getOperationType(), "Succesfully withdrawn " + amount + "$", true);
                }
                else
                {
                    messageDTO = new MessageDTO(moneyDTO.getOperationType(), "Brak wybranej ilosci srodkow na koncie ", true);
                }
             }
             catch(ArgumentException ex)
             {
                 messageDTO = new MessageDTO(moneyDTO.getOperationType(), ex.Message, true);
             }
             return messageDTO;
        }

        private DTO GetBalance(MoneyDTO moneyDTO)
        {
             double amount = account.GetBalance();
             MessageDTO messageDTO = new MessageDTO(moneyDTO.getOperationType(),amount + "$", true);
             return messageDTO;
        }

        public DTO performOperation(DTO dto)
        {
            DTO answer = null;
            if (dto is MoneyDTO)
            {
                MoneyDTO moneyDTO = dto as MoneyDTO;
                
                switch (dto.getOperationType())
                {
                    case 2:
                        answer=Deposit(moneyDTO);
                        break;
                    case 3:
                        answer = Withdraw(moneyDTO);
                        break;

                    case 4:
                        answer = GetBalance(moneyDTO);
                        break;

                    default:
                        throw new Exception("Incorrect operation type");   
                }
            }
            else
            {
                throw new Exception("Incorrect DTO");
            }
            return answer;
        }
    }
}
