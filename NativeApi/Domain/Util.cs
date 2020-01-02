using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeApi.Domain
{
    class Util
    {
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
