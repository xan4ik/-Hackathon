namespace CommandDLL
{
    public interface IDocumentBuilder
    {
        void SetDocumentName(string name);
        IDocument CreateDocument();
    }

}

