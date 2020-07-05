using System;

namespace ShoppingCart.DB
{
    public class AuthHash 
    {
        public static string GetSimpleHash(string username)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(username);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}
