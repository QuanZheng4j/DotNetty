using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetty
{
    public class CardClient : IClient
    {
        private IEventLoopGroup loop = new MultithreadEventLoopGroup(1);
        private string remoteAddress;
        private int remotePort;
        private string localAddress;
        private int localPort;

        public CardClient(string remoteAddress, int remotePort = 10004)
        {
            this.remoteAddress = remoteAddress;
            this.remotePort = remotePort;
        }

        public CardClient(string localAddress, string remoteAddress, int RemotePort = 1001)
        {
            this.localAddress = localAddress;
            this.remoteAddress = remoteAddress;
            this.remotePort = RemotePort;
        }

        public CardClient(string localAddress, int localPort, string remoteAddress, int RemotePort)
        {
            this.localAddress = localAddress;
            this.remoteAddress = remoteAddress;
            this.remotePort = RemotePort;
            this.localPort = localPort;
        }

        public async void Connect()
        {
            await createBootstrap(new Bootstrap(), loop);
        }

        private async Task<Bootstrap> createBootstrap(Bootstrap bootstrap, IEventLoopGroup eventLoop)
        {
            if (bootstrap != null)
            {
                bootstrap.Group(eventLoop);
                bootstrap.Channel<TcpSocketChannel>();
                bootstrap.Option(ChannelOption.TcpNodelay, true);
                bootstrap.Option(ChannelOption.SoReuseaddr, true);
                if (!string.IsNullOrEmpty(localAddress))
                {
                    bootstrap.LocalAddress(new IPEndPoint(IPAddress.Parse(localAddress), localPort));
                }
                bootstrap.Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast("reconnect", new ReconnectHandler(this));
                    pipeline.AddLast("One", new CardFrame());
                   // pipeline.AddLast("Two", new CardFile());
                }));
            }
            while (true)
            {
                try
                {
                    IChannel bootstrapChannel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(remoteAddress), remotePort));
                    break;
                }
                catch (Exception e)
                {
                   // Console.WriteLine(e.Message);
                    Thread.Sleep(1000);
                }
                
            }

            return bootstrap;
        }

        public void ReConnect()
        {
            this.Connect();
        }

    }

    public interface IClient
    {
        // 客户端需要重连
        void ReConnect();
    }
}
