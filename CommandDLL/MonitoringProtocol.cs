using System.IO;

namespace CommandDLL
{
    class MonitoringProtocol : IProtocol
    {
        public string name { get => "Протокол мониторинга"; }
        public byte[] body { get; set; }
        public string html_text { get; private set; }
        public void SetHtml(int seans)
        {
            var html = File.ReadAllText("html/monitoring.html");
            html.Replace(_seans_number_, seans);
            //other properties?
            html_text = html;
        }
    }
}