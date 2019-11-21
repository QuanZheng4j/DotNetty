using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotNetty
{
    public class HelperClasss
    {
        public static string GetAddress(EndPoint end)
        {
            try
            {
                if (end==null)
                {
                    return "";
                }
                IPEndPoint end1 = (IPEndPoint)end;
                var str = end1.Address.ToString();
                str=str.Replace("::ffff:","");
                return str;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
