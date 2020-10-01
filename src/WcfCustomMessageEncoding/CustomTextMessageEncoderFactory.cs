using System.ServiceModel.Channels;

namespace WcfCustomMessageEncoding
{
    public class CustomTextMessageEncoderFactory : MessageEncoderFactory
    {
        private readonly MessageEncoder _encoder;
        private readonly MessageVersion _version;

        internal CustomTextMessageEncoderFactory(string mediaType, string charSet, MessageVersion version)
        {
            _version = version;
            MediaType = mediaType;
            CharSet = charSet;
            _encoder = new CustomTextMessageEncoder(this);
        }

        public override MessageEncoder Encoder => _encoder;

        public override MessageVersion MessageVersion => _version;

        internal string MediaType { get; }

        internal string CharSet { get; }
    }
}
