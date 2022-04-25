using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDLL
{
    class MessageBuilder
    {
        private List<IProtocol> docs;
        private IDocumentBuilder builder;
        public MessageBuilder(IDocumentBuilder builder)
        {
            docs = new List<IProtocol>();
            this.builder = builder;
        }
        public List<IProtocol> GetDocuments()
        {
            return docs;
        }
        public void AddDocument(ProtocolType type, int seans)
        {
            IProtocol protocol;
            switch (type)
            {
                case ProtocolType.Non_standard:
                    protocol = new NonStandardProtocol();
                    protocol.SetHtml(seans);
                    protocol.body = builder.generateDocument(protocol.html_text);
                    docs.Add(protocol);
                    break;
                case ProtocolType.Monitoring:
                    protocol = new MonitoringProtocol();
                    protocol.SetHtml(seans);
                    protocol.body = builder.generateDocument(protocol.html_text);
                    docs.Add(protocol);
                    break;
                case ProtocolType.Allowance:
                    protocol = new AllowanceProtocol();
                    protocol.SetHtml(seans);
                    protocol.body = builder.generateDocument(protocol.html_text);
                    docs.Add(protocol);
                    break;
            }
        }
        public enum ProtocolType
        {
            Non_standard,
            Monitoring,
            Allowance
        }
    }
}
