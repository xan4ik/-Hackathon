using System.IO;

namespace CommandDLL
{
    class NonStandardProtocol : IProtocol
    {
        public string name { get => "Нестандартный протокол"; }
        public byte[] body { get; set; }
        public string html_text { get; private set; }
        public void SetHtml(int seans)
        {
            var html = File.ReadAllText("html/non_standard.html");
            html.Replace(_seans_number_, seans);
            //other properties?
            html_text = html;
        }
    }
}