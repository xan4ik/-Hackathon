namespace CommandDLL
{
    interface IProtocol
    {
        public string name { get; }
        public byte[] body { get; set; }
        public string html_text { get; }
        public void SetHtml(int seans);
    }
}