using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeApi.Domain
{
    class Util
    {
        // special chars for Sabre 
        public static  char CHLOR = (char)0xA5;   // Cross-of-Lorraine
        public static  char ENDITEM = (char)0xA7; // End Item key
        public static  char CHGKEY = (char)0xA4;  // Change Key

        /*
         * Returns the Endpoint based on what environment the Token is for CERT Or Prod 
         */
        public static String ReadToken(string token)
        {

            if (token.Contains("CRT.LB"))
            {
                Console.Write("CERT TOKEN !! ");
                return "https://sws-crt.cert.havail.sabre.com";
            }
            else if (token.Contains("RES.LB"))
            {
                Console.Write("Prod token !!!");
                return "https://webservices.havail.sabre.com";
            }
            return "";

        }


    }
}
