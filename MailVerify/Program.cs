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
            var consoleForegroundColor=Console.ForegroundColor;
            switch (f)
            {
                case MailAddressStatus.Exists:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(args[0] + " Exists.");
                    break;
                case MailAddressStatus.NotExists:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(args[0] + " Does not exist.");
                    break;
                case MailAddressStatus.Uncertain:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(args[0] + " Could not be verified.");
                    break;
                default:
                    break;
            }
            Console.ForegroundColor = consoleForegroundColor;
        }
    }
}
