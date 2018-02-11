using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cimobgrupo2.Extensions
{
    /// <summary>Extensão para hash de passwords</summary>
    public static class PasswordHashExtensions
    {
        /// <summary>Método para dar hash numa password</summary>
        /// <param name="input">Password em texto integral</param>
        /// <returns>Password hashed</returns>
        public static string Encode(string input)
        {
            HMACSHA1 myhmacsha1 = new HMACSHA1(Encoding.ASCII.GetBytes("eswt4g2"));
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            return myhmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }
    }
}
