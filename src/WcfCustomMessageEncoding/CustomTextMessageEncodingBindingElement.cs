using System;
using System.Xml;
using System.ServiceModel.Channels;

namespace WcfCustomMessageEncoding
{
    public class CustomTextMessageEncodingBindingElement : MessageEncodingBindingElement
    {
        private MessageVersion msgVersion;
        private string mediaType;
        private string encoding;

        CustomTextMessageEncodingBindingElement(CustomTextMessageEncodingBindingElement binding)
            : this(binding.Encoding, binding.MediaType, binding.MessageVersion)
        {
            ReaderQuotas = new XmlDictionaryReaderQuotas();
            binding.ReaderQuotas.CopyTo(ReaderQuotas);
        }

        public CustomTextMessageEncodingBindingElement(string encoding, string mediaType, MessageVersion msgVersion)
        {
            this.msgVersion = msgVersion ?? throw new ArgumentNullException("msgVersion");
            this.mediaType = mediaType ?? throw new ArgumentNullException("mediaType");
            this.encoding = encoding ?? throw new ArgumentNullException("encoding");
            ReaderQuotas = new XmlDictionaryReaderQuotas();
        }


        public CustomTextMessageEncodingBindingElement(string encoding, string mediaType)
            : this(encoding, mediaType, MessageVersion.Soap11)
        {

        }

        public CustomTextMessageEncodingBindingElement(string encoding)
            : this(encoding, "text/xml")
        {

        }

        public CustomTextMessageEncodingBindingElement()
            : this("UTF-8")
        {
        }

        public override MessageVersion MessageVersion
        {
            get => msgVersion;

            set => msgVersion = value ?? throw new ArgumentNullException("value");
        }


        public string MediaType
        {
            get => mediaType;

            set => mediaType = value ?? throw new ArgumentNullException("value");
        }

        public string Encoding
        {
            get => encoding;
            set => this.encoding = value ?? throw new ArgumentNullException("value");
        }

        // This encoder does not enforces any quotas for the unsecure messages. The  
        // quotas are enforced for the secure portions of messages when this encoder 
        // is used in a binding that is configured with security.  
        public XmlDictionaryReaderQuotas ReaderQuotas { get; }

        #region IMessageEncodingBindingElement Members 

        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new CustomTextMessageEncoderFactory(MediaType, Encoding, MessageVersion);
        }

        #endregion


        public override BindingElement Clone()
        {
            return new CustomTextMessageEncodingBindingElement(this);
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            return context.CanBuildInnerChannelFactory<TChannel>();
        }


        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return (T)(object)ReaderQuotas;
            }
            else
            {
                return base.GetProperty<T>(context);
            }
        }

    }
}
