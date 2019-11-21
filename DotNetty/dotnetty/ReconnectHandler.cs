using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetty
{
    public class ReconnectHandler : ChannelHandlerAdapter
    {
        private IClient client;
        public ReconnectHandler(IClient client)
        {
            this.client = client;
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);
            //使用键值对，记住所有连接
            ServerConn.ServersDic.AddOrUpdate(HelperClasss.GetAddress(context.Channel.RemoteAddress), context, (k, v) => context);
            //根据IP判定状态 使用事件触发
            StateChange stateChange = new StateChange()
            {
                State = 1,
                IpAddress = HelperClasss.GetAddress(context.Channel.RemoteAddress),
            };
            EventBusUtil._eventBus.Post(stateChange);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            try
            {
                //清空连接
                ServerConn.ServersDic.TryRemove(HelperClasss.GetAddress(context.Channel.RemoteAddress), out context);
                // 根据IP判定状态 使用事件触发
                StateChange stateChange = new StateChange()
                {
                    State = 0,
                    IpAddress = HelperClasss.GetAddress(context?.Channel.RemoteAddress),
                };
                EventBusUtil._eventBus.Post(stateChange);
                if (client != null)
                {
                    Thread.Sleep(100);
                    client.ReConnect();
                }
                base.ChannelInactive(context);
            }
            catch (Exception)
            {
                //log
            }
        }
    }
}
