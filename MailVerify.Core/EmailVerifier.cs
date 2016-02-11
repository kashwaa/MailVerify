using MailVerify.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MailVerify
{
    public class EmailVerifier
    {
        private IDnsResolver Resolver;
        public EmailVerifier(IDnsResolver resolver)
        {
            this.Resolver = resolver;
        }
        public MailAddressStatus Verify(string email)
        {
            try
            {
                var mxRecords = Resolver.GetMXRecords(email.Split('@').Last());
                SMTP smtp = new SMTP(mxRecords);
                return smtp.Verify(email);
            }
            catch (Exception)
            {
                return MailAddressStatus.NotExists;
            }
            
        } 

        
    }
    public enum MailAddressStatus
    {
        Exists, NotExists, Uncertain
    }
    public class SMTP
    {
       
        NetworkStream stream;
        TcpClient client;
        public SMTP(IEnumerable<string> servers)
        {
            foreach (var server in servers)
            {
                try
                {
                    client = new TcpClient(server, 25);
                    break;
                }
                catch (Exception)
                {
                    
                    
                }
                throw new Exception("connection failed");
            }
            
            stream = client.GetStream();
            
        }
        public MailAddressStatus Verify(string email)
        {
            var welcomeMEssage = GetResponse();

            SendCommand("HELO kashwaa.com");
            var heloResponse = GetResponse();
            if (!heloResponse.StartsWith("250")) return MailAddressStatus.Uncertain;

            SendCommand("MAIL FROM: <k@kashwaa.com>");
            var mailfromResponse = GetResponse();
            if (!mailfromResponse.StartsWith("250")) return MailAddressStatus.Uncertain;

            SendCommand("RCPT TO: <emailthatmostlikelydoesntexistonthisserver@" + email.Split('@').Last() + ">");
            var catchallResponse = GetResponse();
            if (catchallResponse.StartsWith("250")) return MailAddressStatus.Uncertain;

            SendCommand("RCPT TO: <" + email + ">");
            var rcptResponse = GetResponse();
            if (!rcptResponse.StartsWith("250")) return MailAddressStatus.NotExists;

            return MailAddressStatus.Exists;
        }
        void SendCommand(string command)
        {
            
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine(command);
            sw.Flush();
            
        }
        string GetResponse()
        {
            StreamReader sr = new StreamReader(stream);
            StringBuilder sb = new StringBuilder();
            while (sr.Peek() != -1)
            {
                sb.Append((char)sr.Read());
            }

            return sb.ToString();
        }
      
    }
}
