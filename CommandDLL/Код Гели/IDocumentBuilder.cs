using System.Net.Mime;

namespace CommandDLL
{
    interface IDocumentBuilders
    {
        public ContentType ct { get; }
        byte[] generateDocument(string html_path);
    }
}