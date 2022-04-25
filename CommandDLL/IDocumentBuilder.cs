using System.Net.Mime;

namespace CommandDLL
{
    interface IDocumentBuilder
    {
        public ContentType ct { get; }
        byte[] generateDocument(string html_path);
    }
}