using System;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;
using System.IO;

namespace WcfCustomMessageEncoding
{
    public class CustomTextMessageEncoder : MessageEncoder
    {
        private readonly CustomTextMessageEncoderFactory _factory;
        private readonly XmlWriterSettings _writerSettings;
        private readonly string _contentType;

        public CustomTextMessageEncoder(CustomTextMessageEncoderFactory factory)
        {
            _factory = factory;
            _writerSettings = new XmlWriterSettings();
            _writerSettings.Encoding = Encoding.GetEncoding(factory.CharSet);
            _contentType = string.Format("{0}; charset={1}",_factory.MediaType, _writerSettings.Encoding.HeaderName);
        }

        public override string ContentType => _contentType;

        public override string MediaType => _factory.MediaType;

        public override MessageVersion MessageVersion => _factory.MessageVersion;

        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            var msgContents = new byte[buffer.Count];
            Array.Copy(buffer.Array, buffer.Offset, msgContents, 0, msgContents.Length);
            bufferManager.ReturnBuffer(buffer.Array);

            var stream = new MemoryStream(msgContents);
            return ReadMessage(stream, int.MaxValue);
        }

        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            var reader = XmlReader.Create(stream);
            return Message.CreateMessage(reader, maxSizeOfHeaders, this.MessageVersion);
        }

        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            var stream = new MemoryStream();
            var writer = XmlWriter.Create(stream, _writerSettings);
                message.WriteMessage(writer);
                writer.Close();

            var messageBytes = stream.GetBuffer();
            var messageLength = (int)stream.Position;
                stream.Close();

            var totalLength = messageLength + messageOffset;
            var totalBytes = bufferManager.TakeBuffer(totalLength);
            Array.Copy(messageBytes, 0, totalBytes, messageOffset, messageLength);

            var byteArray = new ArraySegment<byte>(totalBytes, messageOffset, messageLength);
            return byteArray;
        }

        public override void WriteMessage(Message message, Stream stream)
        {
            var writer = XmlWriter.Create(stream, _writerSettings);
            message.WriteMessage(writer);
            writer.Close();
        }

        public override bool IsContentTypeSupported(string contentType)
        {
            if (base.IsContentTypeSupported(contentType))
                return true;
            
            if (contentType.Length == this.MediaType.Length)
                return contentType.Equals(this.MediaType, StringComparison.OrdinalIgnoreCase);
      
            else if (contentType.StartsWith(this.MediaType, StringComparison.OrdinalIgnoreCase) && (contentType[this.MediaType.Length] == ';'))
                return true;
                
            return false;
        }
    }
}
