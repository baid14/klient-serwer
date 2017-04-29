using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class DTOUtil
    {
        public static DTO Login(AccConnectionController accConnectionController,ref Account account,ref bool loged,DTO dto)
        {
            MessageDTO messageDTO=null;
            if (dto is UserDTO && dto.getOperationType() == 1)
            {
                if (loged == true && dto.getOperationType() == 1)
                {
                    messageDTO = new MessageDTO(dto.getOperationType(), "You are already logged", false);
                }
                try
                {
                    account = accConnectionController.LogIn(dto);
                    loged = true;
                    messageDTO = new MessageDTO(dto.getOperationType(), "Welcome! You are now loged in.", true);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    messageDTO = new MessageDTO(dto.getOperationType(), msg, false);
                }
            }
            else
            {
                messageDTO = new MessageDTO(dto.getOperationType(), "First you must log in!", false);
            }
            return messageDTO;
        }

        public static DTO OperateOnAccount(AccOperationController accOperationController,Account account,DTO dto)
        {
             DTO answer=null;
             if(account!=null)
             {
                  if(accOperationController==null)
                  {
                        accOperationController = new AccOperationController(account);
                  }
                  answer=accOperationController.performOperation(dto);
             }
            return answer;
        }

        public static DTO Logout(DTO dto, ref Account account, ref bool loged)
        {
            DTO answer=null;
            account = null;
            loged = false;
            answer = new MessageDTO(dto.getOperationType(), "Loged out correctly", true);
            return answer;
        }
    }
}
