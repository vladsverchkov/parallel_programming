using System;
using System.Net;

namespace NetWork.NetworkLib 
{
    public static class NetworkFunctions 
    {
        public static bool CheckIPv4Valid(string strIP) //перевіряє, чи валідна ір-адреса
        {
            //  перевірка ір-адреси за октетами
            char chrFullStop = '.'; 
            string[] arrOctets = strIP.Split(chrFullStop); 
            if (arrOctets.Length != 4) 
            {
                return false;
            }
            //перевірка кожної підстроки, чи значення int менше, ніж 255 та чи довжина char[]  !> 2 
            
            Int16 MAXVALUE = 255; 
            Int32 temp;
            foreach (string strOctet in arrOctets)
            {
                if (strOctet.Length > 3)
                {
                    return false; 
                }

                temp = int.Parse(strOctet); 
                if (temp > MAXVALUE) 
                {
                    return false; 
                }
            }
            return true; 
        }

        //перевірка порту на валідність
        public static bool CheckPortValid(string strPort) 
        {
            int port; 

            if (int.TryParse(strPort, out port)) 
            {
                if (port >= 1 && port <= 65535)
                    return true; 
            }

            return false; //returns false
        }

        //отримання ір-адреси
        public static string GetIP4Address()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress i in ips)
            {
                if (i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) 
                    return i.ToString();
            }
            return "127.0.0.1";
        }

    } 
} 
