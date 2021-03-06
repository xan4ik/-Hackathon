using OpenHtmlToPdf;
using System;
using System.IO;
using System.Text;

namespace CommandDLL
{
    public abstract class DocumentBuilderPDF : IDocumentBuilder
    {
        private string documentName;
        private StringBuilder builder;

        public void LoadTemplate(string templatePath)
        {
            builder = new StringBuilder(File.ReadAllText(templatePath));
        }

        protected void TryReplaceSring(string from, string to) 
        {
            if (builder is null) 
            {
                throw new Exception("Template did't load");
            }

            builder.Replace(from, to);
        }

        public void SetDocumentName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Invalid document name");
            }

            documentName = name;
        }

        public IDocument CreateDocument()
        {
            if (string.IsNullOrEmpty(documentName)) 
            {
                throw new Exception("Invalid document name");
            }

            var documentContent = Pdf
                .From(builder.ToString())
                .OfSize(PaperSize.A4)
                .WithoutOutline()
                .WithMargins(1.25.Centimeters())
                .Portrait()
                .Comressed()
                .Content();

            return new SimpleDocument(documentName, documentContent);
        }
    }

}

