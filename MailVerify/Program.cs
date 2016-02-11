using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailVerify
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count()!=1)
            {
                Console.WriteLine("Usage: mailverify email");
                Environment.Exit(1);
            }
            var f = new MailVerify.EmailVerifier(new WebTools.DnsMx()).Verify(args[0]);
            Console.WriteLine(f.ToString());
        }
    }
}
