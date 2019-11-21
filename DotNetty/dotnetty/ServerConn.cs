using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetty
{
    public class ServerConn
    {
        /// <summary>
        /// 所有设备连接  key Ipaddress,Value 连接
        /// </summary>
        public static ConcurrentDictionary<string, IChannelHandlerContext> ServersDic = new ConcurrentDictionary<string, IChannelHandlerContext>();

        public static void SendData(byte[] data, string ipaddress)
        {
            if (ServersDic.ContainsKey(ipaddress))
            {
                var channel = ServersDic[ipaddress];
                IByteBuffer buffer = Unpooled.CopiedBuffer(data);
                channel.WriteAndFlushAsync(buffer);
                // return true;
            }
            else
            {
                // return false;
            }
        }

        public static void Close(string ipaddress)
        {
            if (ServersDic.ContainsKey(ipaddress))
            {
                var channel = ServersDic[ipaddress];
                channel.CloseAsync();
            }
        }

        #region 发送文件方法
        public static void sendToServer(string path,string ipaddress,byte[] head,byte[] fileLen)
        {


            //创建文件流
            FileStream file = File.Open(path, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);

            try
            {
                SendData(head, ipaddress);
                SendData(fileLen, ipaddress);

                byte[] b = new byte[1024];

                int n = 0;
                while ((n=file.Read(b,0,b.Length))>0)
                {
                    SendData(b, ipaddress);
                }
                file.Close();
            }
            catch (Exception e)
            {
                file.Close();
            }
        }
        #endregion


    }
}
