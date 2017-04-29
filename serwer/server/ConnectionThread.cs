using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace server
{
    class ConnectionThread
    {
        TcpClient client;
        NetworkStream stream;
        Database db;

        public ConnectionThread(TcpClient client, Database db)
        {
            this.client = client;
            stream = client.GetStream();
            this.db = db;
        }

        public void connectionHandling()
        {
            Console.WriteLine("Tworze watek dla polaczenia");
            int max = 65565;
            Byte[] bytes = new Byte[max];
            int i;
            bool loged = false;
            AccConnectionController accConnectionController = new AccConnectionController(db);
            AccOperationController accOperationController = null;
            Account account = null;
            DTO answer;

            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                string className = ByteParser.byteToType(bytes);
                DtoFactory dtoFactory = new DtoFactory();
                DTO dto = dtoFactory.getDTO(className);
                dto.fromByteArray(bytes);
                answer = null;
                if(dto!=null)
                {
                    if(loged==false)
                    {
                        answer = DTOUtil.Login(accConnectionController,ref account,ref loged,dto);
                    }
                    else if(loged==true && dto.getOperationType() == 1)
                    {
                        answer = DTOUtil.Login(accConnectionController, ref account, ref loged, dto);
                        
                    }
                    else
                    {
                        if (dto.getOperationType() == 5)
                            answer = DTOUtil.Logout(dto, ref account, ref loged);
                        else
                            answer = DTOUtil.OperateOnAccount(accOperationController, account, dto);
                    }
                }
                byte[] data = answer.toByteArray();
                stream.Write(data, 0, data.Length);
            }
            client.Close();
        }
    }
}



/*
                    if(loged==false && dto is UserDTO && dto.getOperationType()==1)
                    {
                        try
                        {
                            account=accConnectionController.LogIn(dto);
                            loged = true;
                            MessageDTO messageDTO = new MessageDTO(dto.getOperationType(),"Welcome! You are now loged in.", true);
                            byte[] data = messageDTO.toByteArray();
                            stream.Write(data, 0, data.Length);
                        }
                        catch(Exception ex)
                        {
                            string msg=ex.Message;
                            MessageDTO messageDTO = new MessageDTO(dto.getOperationType(),msg,false);
                            byte[] data=messageDTO.toByteArray();
                            stream.Write(data, 0, data.Length);
                        }
                    }
                    else if (loged==false)
                    {
                        MessageDTO messageDTO=new MessageDTO(dto.getOperationType(),"First you must log in!",false);
                        byte[] data = messageDTO.toByteArray();
                        stream.Write(data, 0, data.Length);
                    }
                
                    else if(loged==true && account!=null)
                    {
                        if(accOperationController==null)
                        {
                            accOperationController = new AccOperationController(account);
                        }
                        DTO clientAnswer=accOperationController.performOperation(dto);
                        byte[] data = clientAnswer.toByteArray();
                        stream.Write(data, 0, data.Length);
                    }
                    else if (loged==true && dto is MessageDTO && dto.getOperationType()==5)
                    {
                        DTO answer=accConnectionController.LogOut(dto);
                        byte[] data = answer.toByteArray();
                        stream.Write(data, 0, data.Length);
                        break;
                    }
                    */