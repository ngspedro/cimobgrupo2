using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cimobgrupo2.Extensions
{
    public static class PasswordHashExtensions
    {
        public static string Encode(string input)
        {
            HMACSHA1 myhmacsha1 = new HMACSHA1(Encoding.ASCII.GetBytes("eswt4g2"));
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            return myhmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }
    }
}
