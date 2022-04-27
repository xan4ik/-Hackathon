using System.IO;
using System.Threading.Tasks;

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

        public async Task SaveAsync(string path)
        {
            await File.WriteAllBytesAsync(Path.Combine(path, name), content);
        }
    }

}

