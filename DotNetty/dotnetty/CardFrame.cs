using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System.Collections.Generic;
using System.Text;

namespace DotNetty
{
    public class CardFrame : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            var data = input.ToArray();
            input.ResetWriterIndex();
            EventBusUtil._eventBus.Post(Encoding.Default.GetString(data));
        }

    }
}
