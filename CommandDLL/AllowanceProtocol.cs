using System.IO;

namespace CommandDLL
{
    class AllowanceProtocol : IProtocol
    {
        public string name { get => "Протокол допуска"; }
        public byte[] body { get; set; }
        public string html_text { get; private set; }
        public void SetHtml(int seans)
        {
            var html = File.ReadAllText("html/allowance.html");
            html.Replace(_seans_number_, seans);
            //other properties?
            html_text = html;
        }
    }
}