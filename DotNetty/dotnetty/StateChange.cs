using System;

namespace DotNetty
{
    public class StateChange
    {
        public string IpAddress { get; set; }

        /// <summary>
        /// 0 断开  1 连接
        /// </summary>
        public int State { get; set; }

        public DateTime DateTime { get; set; }
    }
}
