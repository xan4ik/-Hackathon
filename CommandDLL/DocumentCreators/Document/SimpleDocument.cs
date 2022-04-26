using System.IO;

namespace CommandDLL
{
    public class SimpleDocument : IDocument
    {
        private byte[] content;
        private string name;

        public SimpleDocument(string name, byte[] content)
        {
            this.content = content;
            this.name = name;
        }

        public string Name 
        {
            get 
            {
                return name;
            } 
        }

        public byte[] GetDocumntContent()
        {
            return content;
        }

        public void Save(string path)
        {
            File.WriteAllBytes(Path.Combine(path, name), content);
        }
    }

}

