using OpenHtmlToPdf;
using System.IO;
using System.Net.Mime;

namespace CommandDLL
{
    class BuilderPDF : IDocumentBuilder
    {
        public ContentType ct { get => new ContentType(MediaTypeNames.Application.Pdf); }
        public byte[] generateDocument(string html)
        {
            var pdf = Pdf
                .From(html)
                .OfSize(PaperSize.A4)
                .WithoutOutline()
                .WithMargins(1.25.Centimeters())
                .Portrait()
                .Comressed()
                .Content();
            return pdf;
        }
    }
}