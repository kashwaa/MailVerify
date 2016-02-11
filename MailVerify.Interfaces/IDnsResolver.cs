using System;
namespace MailVerify.Interfaces
{
    public interface IDnsResolver
    {
        string[] GetMXRecords(string domain);
    }
}
